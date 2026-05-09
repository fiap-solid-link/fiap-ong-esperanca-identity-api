using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Usuarios;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.AlterarSenha;

public sealed class AlterarSenhaHandler(
    IUsuarioRepository usuarioRepository,
    IUsuarioAutenticado usuarioAutenticado,
    IPasswordHasher passwordHasher,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<AlterarSenhaCommand, Result<object>>
{
    public async Task<Result<object>> Handle(AlterarSenhaCommand request, CancellationToken ct)
    {
        var usuarioId = usuarioAutenticado.ObterUsuarioId();
        if (usuarioId is null)
            return Result<object>.Unauthorized(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        var usuario = await usuarioRepository.ObterPorIdAsync(usuarioId.Value, ct);
        if (usuario is null)
            return Result<object>.NotFound(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        if (!passwordHasher.Verificar(request.SenhaAtual, usuario.SenhaHash))
            return Result<object>.Fail(localizer[IdentityErrorCodes.SenhaAtualInvalida]);

        var novaSenhaHash = passwordHasher.Hash(request.NovaSenha);
        usuario.AlterarSenha(novaSenhaHash);

        await usuarioRepository.AtualizarAsync(usuario, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<object>.Ok(new { });
    }
}
