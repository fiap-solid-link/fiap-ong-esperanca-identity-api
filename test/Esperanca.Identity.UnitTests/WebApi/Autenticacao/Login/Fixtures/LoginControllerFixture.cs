using Esperanca.Identity.WebApi.Autenticacao.Login;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login.Fixtures;

public sealed class LoginControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public LoginController CriarController()
    {
        return new LoginController(Mediator);
    }
}
