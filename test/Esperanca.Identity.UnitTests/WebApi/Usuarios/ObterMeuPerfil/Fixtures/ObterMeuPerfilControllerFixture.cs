using Esperanca.Identity.WebApi.Usuarios.ObterMeuPerfil;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ObterMeuPerfil.Fixtures;

public sealed class ObterMeuPerfilControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public ObterMeuPerfilController CriarController()
    {
        return new ObterMeuPerfilController(Mediator);
    }
}
