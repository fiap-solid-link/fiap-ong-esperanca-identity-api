using Esperanca.Identity.Application._Shared;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ObterMeuPerfil.Mocks;

public sealed class UsuarioAutenticadoMock
{
    public IUsuarioAutenticado Instance { get; } = Substitute.For<IUsuarioAutenticado>();

    public void SetupObterUsuarioId(Guid? usuarioId)
    {
        Instance.ObterUsuarioId().Returns(usuarioId);
    }
}
