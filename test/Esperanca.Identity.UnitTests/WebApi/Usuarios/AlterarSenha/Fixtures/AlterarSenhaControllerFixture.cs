using Esperanca.Identity.WebApi.Usuarios.AlterarSenha;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AlterarSenha.Fixtures;

public sealed class AlterarSenhaControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public AlterarSenhaController CriarController()
    {
        return new AlterarSenhaController(Mediator);
    }
}
