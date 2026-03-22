using Esperanca.Identity.Application.Autenticacao.ObterMeuPerfil;
using Esperanca.Identity.UnitTests.Application.Autenticacao.ObterMeuPerfil.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.ObterMeuPerfil.Fixtures;

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
