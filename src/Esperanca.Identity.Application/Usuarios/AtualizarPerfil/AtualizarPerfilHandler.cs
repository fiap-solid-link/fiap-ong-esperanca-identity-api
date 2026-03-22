using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Usuarios;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

public sealed class AtualizarPerfilHandler(
    IUsuarioRepository usuarioRepository,
    IUsuarioAutenticado usuarioAutenticado,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<AtualizarPerfilCommand, Result<AtualizarPerfilResponse>>
{
    public async Task<Result<AtualizarPerfilResponse>> Handle(AtualizarPerfilCommand request, CancellationToken ct)
    {
        var usuarioId = usuarioAutenticado.ObterUsuarioId();
        if (usuarioId is null)
            return Result<AtualizarPerfilResponse>.Unauthorized(localizer[IdentityErrorCodes.TokenInvalido]);

        var usuario = await usuarioRepository.ObterPorIdAsync(usuarioId.Value, ct);
        if (usuario is null)
            return Result<AtualizarPerfilResponse>.NotFound(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        usuario.AtualizarPerfil(request.Nome, request.Apelido);
        await usuarioRepository.AtualizarAsync(usuario, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<AtualizarPerfilResponse>.Ok(
            new AtualizarPerfilResponse(usuario.Id, usuario.Nome, usuario.Apelido));
    }
}
