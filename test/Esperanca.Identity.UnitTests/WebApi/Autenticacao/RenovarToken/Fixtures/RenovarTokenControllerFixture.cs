using Esperanca.Identity.WebApi.Autenticacao.RenovarToken;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken.Fixtures;

public sealed class RenovarTokenControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public RenovarTokenController CriarController()
    {
        return new RenovarTokenController(Mediator);
    }
}
