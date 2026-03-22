using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Domain.Usuarios.Enums;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.RevogarGestor;

public sealed class RevogarGestorHandler(
    IUsuarioRepository usuarioRepository,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<RevogarGestorCommand, Result<RevogarGestorResponse>>
{
    public async Task<Result<RevogarGestorResponse>> Handle(RevogarGestorCommand request, CancellationToken ct)
    {
        var usuario = await usuarioRepository.ObterPorIdAsync(request.UsuarioId, ct);
        if (usuario is null)
            return Result<RevogarGestorResponse>.NotFound(localizer[IdentityErrorCodes.UsuarioNaoEncontrado]);

        if (!usuario.PossuiRole(RoleTipo.GestorONG))
            return Result<RevogarGestorResponse>.Fail(localizer[IdentityErrorCodes.UsuarioNaoPossuiGestor]);

        usuario.RemoverRole(RoleTipo.GestorONG);
        await usuarioRepository.AtualizarAsync(usuario, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<RevogarGestorResponse>.Ok(
            new RevogarGestorResponse(usuario.Id, "Perfil GestorONG revogado com sucesso."));
    }
}
