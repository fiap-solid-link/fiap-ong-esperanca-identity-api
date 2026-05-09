using Esperanca.Identity.Application.Autenticacao.Registrar;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Fixtures;

public sealed class RegistrarHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public RoleRepositoryMock RoleRepositoryMock { get; } = new();
    public PasswordHasherMock PasswordHasherMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public RegistrarHandler CriarHandler()
    {
        return new RegistrarHandler(
            UsuarioRepositoryMock.Instance,
            RoleRepositoryMock.Instance,
            PasswordHasherMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
