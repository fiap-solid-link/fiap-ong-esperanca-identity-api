using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Domain.Usuarios.Enums;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.Registrar;

public sealed class RegistrarHandler(
    IUsuarioRepository usuarioRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IAppDbContext dbContext,
    IAppLocalizer localizer) : IRequestHandler<RegistrarCommand, Result<RegistrarResponse>>
{
    public async Task<Result<RegistrarResponse>> Handle(RegistrarCommand request, CancellationToken ct)
    {
        if (await usuarioRepository.EmailExisteAsync(request.Email, ct))
            return Result<RegistrarResponse>.Fail(localizer[IdentityErrorCodes.EmailJaCadastrado]);

        var senhaHash = passwordHasher.Hash(request.Senha);
        var usuario = new Usuario(request.Nome, request.Email, senhaHash);

        var roleDoador = await roleRepository.ObterPorTipoAsync(RoleTipo.Doador, ct);
        if (roleDoador is not null)
            usuario.AdicionarRole(roleDoador);

        await usuarioRepository.AdicionarAsync(usuario, ct);
        await dbContext.SaveChangesAsync(ct);

        return Result<RegistrarResponse>.Created(
            new RegistrarResponse(usuario.Id, usuario.Nome, usuario.Email));
    }
}
