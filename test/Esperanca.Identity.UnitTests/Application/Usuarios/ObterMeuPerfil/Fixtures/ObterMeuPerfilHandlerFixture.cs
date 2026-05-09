using Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;
using Esperanca.Identity.UnitTests.Application.Usuarios.ObterMeuPerfil.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ObterMeuPerfil.Fixtures;

public sealed class ObterMeuPerfilHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public UsuarioAutenticadoMock UsuarioAutenticadoMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public ObterMeuPerfilHandler CriarHandler()
    {
        return new ObterMeuPerfilHandler(
            UsuarioRepositoryMock.Instance,
            UsuarioAutenticadoMock.Instance,
            AppLocalizerMock.Instance);
    }
}
