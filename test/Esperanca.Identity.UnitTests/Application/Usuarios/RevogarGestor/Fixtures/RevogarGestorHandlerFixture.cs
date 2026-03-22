using Esperanca.Identity.Application.Usuarios.RevogarGestor;
using Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor.Fixtures;

public sealed class RevogarGestorHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public RevogarGestorHandler CriarHandler()
    {
        return new RevogarGestorHandler(
            UsuarioRepositoryMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
