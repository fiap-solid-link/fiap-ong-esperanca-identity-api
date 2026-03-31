using Esperanca.Identity.WebApi.Usuarios.RevogarGestor;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.RevogarGestor.Fixtures;

public sealed class RevogarGestorControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public RevogarGestorController CriarController()
    {
        return new RevogarGestorController(Mediator);
    }
}
