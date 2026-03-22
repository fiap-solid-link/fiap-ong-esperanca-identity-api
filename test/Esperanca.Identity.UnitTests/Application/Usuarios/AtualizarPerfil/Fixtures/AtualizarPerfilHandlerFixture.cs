using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;
using Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil.Fixtures;

public sealed class AtualizarPerfilHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public UsuarioAutenticadoMock UsuarioAutenticadoMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public AtualizarPerfilHandler CriarHandler()
    {
        return new AtualizarPerfilHandler(
            UsuarioRepositoryMock.Instance,
            UsuarioAutenticadoMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
