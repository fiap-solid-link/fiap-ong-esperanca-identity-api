using Esperanca.Identity.WebApi.Usuarios.AtualizarPerfil;
using MediatR;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil.Fixtures;

public sealed class AtualizarPerfilControllerFixture
{
    public IMediator Mediator { get; } = Substitute.For<IMediator>();

    public AtualizarPerfilController CriarController()
    {
        return new AtualizarPerfilController(Mediator);
    }
}
