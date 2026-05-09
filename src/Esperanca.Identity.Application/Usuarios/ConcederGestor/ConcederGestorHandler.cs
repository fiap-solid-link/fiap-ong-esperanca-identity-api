using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Domain.Usuarios.Enums;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.ConcederGestor;

public sealed class ConcederGestorHandler(
    IUsuarioRepository usuarioRepository,
    IRoleRepository roleRepository,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<ConcederGestorCommand, Result<ConcederGestorResponse>>
{
    public async Task<Result<ConcederGestorResponse>> Handle(ConcederGestorCommand request, CancellationToken ct)
    {
        var usuario = await usuarioRepository.ObterPorIdAsync(request.UsuarioId, ct);
        if (usuario is null)
            return Result<ConcederGestorResponse>.NotFound(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        if (usuario.PossuiRole(RoleTipo.GestorONG))
            return Result<ConcederGestorResponse>.Fail(localizer[IdentityErrorCodes.UsuarioJaPossuiGestor]);

        var roleGestor = await roleRepository.ObterPorTipoAsync(RoleTipo.GestorONG, ct);
        if (roleGestor is null)
            return Result<ConcederGestorResponse>.Fail(localizer[IdentityErrorCodes.RoleGestorNaoEncontrada]);

        usuario.AdicionarRole(roleGestor);
        await usuarioRepository.AtualizarAsync(usuario, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<ConcederGestorResponse>.Ok(
            new ConcederGestorResponse(usuario.Id, "Perfil GestorONG concedido com sucesso."));
    }
}
