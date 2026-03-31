using Esperanca.Identity.WebApi.Usuarios.ConcederGestor;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ConcederGestor.Fixtures;

public sealed class ConcederGestorControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public ConcederGestorController CriarController()
    {
        return new ConcederGestorController(Mediator);
    }
}
