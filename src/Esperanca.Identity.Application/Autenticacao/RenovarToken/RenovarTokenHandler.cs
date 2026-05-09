using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Autenticacao.Login;
using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.RenovarToken;

public sealed class RenovarTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUsuarioRepository usuarioRepository,
    IJwtService jwtService,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<RenovarTokenCommand, Result<LoginResponse>>
{
    private static readonly TimeSpan RefreshTokenValidade = TimeSpan.FromDays(7);

    public async Task<Result<LoginResponse>> Handle(RenovarTokenCommand request, CancellationToken ct)
    {
        var tokenExistente = await refreshTokenRepository.ObterPorTokenAsync(request.RefreshToken, ct);
        if (tokenExistente is null || !tokenExistente.Ativo)
            return Result<LoginResponse>.Unauthorized(localizer[IdentityErrorCodes.RefreshTokenInvalidoOuExpirado]);

        var usuario = await usuarioRepository.ObterPorIdAsync(tokenExistente.UsuarioId, ct);
        if (usuario is null)
            return Result<LoginResponse>.Unauthorized(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        tokenExistente.Revogar();
        await refreshTokenRepository.AtualizarAsync(tokenExistente, ct);

        var novoAccessToken = jwtService.GerarAccessToken(usuario);
        var novoRefreshTokenValue = jwtService.GerarRefreshToken();
        var novoRefreshToken = new RefreshToken(usuario.Id, novoRefreshTokenValue, RefreshTokenValidade);

        await refreshTokenRepository.AdicionarAsync(novoRefreshToken, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<LoginResponse>.Ok(
            new LoginResponse(novoAccessToken, novoRefreshTokenValue, novoRefreshToken.ExpiraEm));
    }
}
