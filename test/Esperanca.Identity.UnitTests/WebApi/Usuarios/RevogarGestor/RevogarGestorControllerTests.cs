using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Usuarios.RevogarGestor;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.RevogarGestor.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.RevogarGestor.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.RevogarGestor;

public sealed class RevogarGestorControllerTests
{
    [Fact]
    public async Task RevogarGestor_DeveRetornarOk_QuandoResultadoComSucesso()
    {
        // Arrange
        var fixture = new RevogarGestorControllerFixture();
        var usuarioId = Guid.NewGuid();
        var response = RevogarGestorResponseFaker.Valid(usuarioId: usuarioId);

        fixture.Mediator
            .Send(Arg.Any<RevogarGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RevogarGestorResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RevogarGestor(usuarioId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<RevogarGestorResponse>(okResult.Value);
        Assert.Equal(usuarioId, dados.UsuarioId);
        Assert.Equal(response.Mensagem, dados.Mensagem);
    }

    [Fact]
    public async Task RevogarGestor_DeveRetornar404_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new RevogarGestorControllerFixture();
        var usuarioId = Guid.NewGuid();

        fixture.Mediator
            .Send(Arg.Any<RevogarGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RevogarGestorResponse>.NotFound("Usuário não encontrado"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RevogarGestor(usuarioId, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public async Task RevogarGestor_DeveRetornar400_QuandoFalha()
    {
        // Arrange
        var fixture = new RevogarGestorControllerFixture();
        var usuarioId = Guid.NewGuid();

        fixture.Mediator
            .Send(Arg.Any<RevogarGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RevogarGestorResponse>.Fail("Usuário não possui role"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RevogarGestor(usuarioId, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task RevogarGestor_DeveChamarMediatorComCommandCorreto()
    {
        // Arrange
        var fixture = new RevogarGestorControllerFixture();
        var usuarioId = Guid.NewGuid();
        var response = RevogarGestorResponseFaker.Valid(usuarioId: usuarioId);

        fixture.Mediator
            .Send(Arg.Any<RevogarGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RevogarGestorResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.RevogarGestor(usuarioId, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(
            Arg.Is<RevogarGestorCommand>(c => c.UsuarioId == usuarioId),
            Arg.Any<CancellationToken>());
    }
}
