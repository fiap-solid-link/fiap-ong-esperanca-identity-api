using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Usuarios;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;

public sealed class ObterMeuPerfilHandler(
    IUsuarioRepository usuarioRepository,
    IUsuarioAutenticado usuarioAutenticado,
    IAppLocalizer localizer) : IRequestHandler<ObterMeuPerfilQuery, Result<ObterMeuPerfilResponse>>
{
    public async Task<Result<ObterMeuPerfilResponse>> Handle(ObterMeuPerfilQuery request, CancellationToken ct)
    {
        var usuarioId = usuarioAutenticado.ObterUsuarioId();
        if (usuarioId is null)
            return Result<ObterMeuPerfilResponse>.Unauthorized(localizer[IdentityErrorCodes.TokenInvalido]);

        var usuario = await usuarioRepository.ObterPorIdAsync(usuarioId.Value, ct);
        if (usuario is null)
            return Result<ObterMeuPerfilResponse>.NotFound(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        return Result<ObterMeuPerfilResponse>.Ok(new ObterMeuPerfilResponse(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Apelido,
            usuario.Roles.Select(r => r.Nome).ToList()));
    }
}
