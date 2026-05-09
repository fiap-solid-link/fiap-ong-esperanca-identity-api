using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Usuarios.ConcederGestor;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.ConcederGestor.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.ConcederGestor.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ConcederGestor;

public sealed class ConcederGestorControllerTests
{
    [Fact]
    public async Task ConcederGestor_DeveRetornarOk_QuandoResultadoComSucesso()
    {
        // Arrange
        var fixture = new ConcederGestorControllerFixture();
        var usuarioId = Guid.NewGuid();
        var response = ConcederGestorResponseFaker.Valid(usuarioId: usuarioId);

        fixture.Mediator
            .Send(Arg.Any<ConcederGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<ConcederGestorResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ConcederGestor(usuarioId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<ConcederGestorResponse>(okResult.Value);
        Assert.Equal(usuarioId, dados.UsuarioId);
        Assert.Equal(response.Mensagem, dados.Mensagem);
    }

    [Fact]
    public async Task ConcederGestor_DeveRetornar404_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new ConcederGestorControllerFixture();
        var usuarioId = Guid.NewGuid();

        fixture.Mediator
            .Send(Arg.Any<ConcederGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<ConcederGestorResponse>.NotFound("Usuário não encontrado"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ConcederGestor(usuarioId, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public async Task ConcederGestor_DeveRetornar400_QuandoFalha()
    {
        // Arrange
        var fixture = new ConcederGestorControllerFixture();
        var usuarioId = Guid.NewGuid();

        fixture.Mediator
            .Send(Arg.Any<ConcederGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<ConcederGestorResponse>.Fail("Usuário já possui role"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ConcederGestor(usuarioId, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task ConcederGestor_DeveChamarMediatorComCommandCorreto()
    {
        // Arrange
        var fixture = new ConcederGestorControllerFixture();
        var usuarioId = Guid.NewGuid();
        var response = ConcederGestorResponseFaker.Valid(usuarioId: usuarioId);

        fixture.Mediator
            .Send(Arg.Any<ConcederGestorCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<ConcederGestorResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.ConcederGestor(usuarioId, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(
            Arg.Is<ConcederGestorCommand>(c => c.UsuarioId == usuarioId),
            Arg.Any<CancellationToken>());
    }
}
