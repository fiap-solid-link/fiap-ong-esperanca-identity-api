using Esperanca.Identity.Application.Usuarios.ConcederGestor;
using Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Fixtures;

public sealed class ConcederGestorHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public RoleRepositoryMock RoleRepositoryMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public ConcederGestorHandler CriarHandler()
    {
        return new ConcederGestorHandler(
            UsuarioRepositoryMock.Instance,
            RoleRepositoryMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
