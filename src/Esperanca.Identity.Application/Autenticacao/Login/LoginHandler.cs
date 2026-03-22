using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.Login;

public sealed class LoginHandler(
    IUsuarioRepository usuarioRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private static readonly TimeSpan RefreshTokenValidade = TimeSpan.FromDays(7);

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
    {
        var usuario = await usuarioRepository.ObterPorEmailAsync(request.Email, ct);
        if (usuario is null || !passwordHasher.Verificar(request.Senha, usuario.SenhaHash))
            return Result<LoginResponse>.Unauthorized(localizer[IdentityErrorCodes.EmailOuSenhaInvalidos]);

        var accessToken = jwtService.GerarAccessToken(usuario);
        var refreshTokenValue = jwtService.GerarRefreshToken();

        var refreshToken = new RefreshToken(usuario.Id, refreshTokenValue, RefreshTokenValidade);
        await refreshTokenRepository.AdicionarAsync(refreshToken, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<LoginResponse>.Ok(
            new LoginResponse(accessToken, refreshTokenValue, refreshToken.ExpiraEm));
    }
}
