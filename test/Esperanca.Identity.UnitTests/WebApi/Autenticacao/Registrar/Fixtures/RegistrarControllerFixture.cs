using Esperanca.Identity.WebApi.Autenticacao.Registrar;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Registrar.Fixtures;

public sealed class RegistrarControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public RegistrarController CriarController()
    {
        return new RegistrarController(Mediator);
    }
}
