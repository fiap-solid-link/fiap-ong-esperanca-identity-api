using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Domain.Usuarios.Enums;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Mocks;

public sealed class RoleRepositoryMock
{
    public IRoleRepository Instance { get; } = Substitute.For<IRoleRepository>();

    public void SetupObterPorTipoAsync(RoleTipo tipo, Role? role)
    {
        Instance.ObterPorTipoAsync(tipo, Arg.Any<CancellationToken>())
            .Returns(role);
    }
}
