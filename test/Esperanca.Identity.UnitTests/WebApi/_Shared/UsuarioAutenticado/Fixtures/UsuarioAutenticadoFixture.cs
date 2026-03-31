using Esperanca.Identity.UnitTests.WebApi._Shared.UsuarioAutenticado.Mocks;

namespace Esperanca.Identity.UnitTests.WebApi._Shared.UsuarioAutenticado.Fixtures;

public sealed class UsuarioAutenticadoFixture
{
    public HttpContextAccessorMock HttpContextAccessorMock { get; } = new();

    public Identity.WebApi._Shared.UsuarioAutenticado CriarUsuarioAutenticado()
    {
        return new Identity.WebApi._Shared.UsuarioAutenticado(HttpContextAccessorMock.Instance);
    }
}
