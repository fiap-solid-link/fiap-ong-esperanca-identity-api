using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil;

public sealed class AtualizarPerfilControllerTests
{
    [Fact]
    public async Task AtualizarPerfil_DeveRetornarOk_QuandoResultadoComSucesso()
    {
        // Arrange
        var fixture = new AtualizarPerfilControllerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();
        var response = AtualizarPerfilResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AtualizarPerfilCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<AtualizarPerfilResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AtualizarPerfil(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<AtualizarPerfilResponse>(okResult.Value);
        Assert.Equal(response.Id, dados.Id);
        Assert.Equal(response.Nome, dados.Nome);
        Assert.Equal(response.Apelido, dados.Apelido);
    }

    [Fact]
    public async Task AtualizarPerfil_DeveRetornar400_QuandoDadosInvalidos()
    {
        // Arrange
        var fixture = new AtualizarPerfilControllerFixture();
        var command = AtualizarPerfilCommandFaker.Invalid();

        fixture.Mediator
            .Send(Arg.Any<AtualizarPerfilCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<AtualizarPerfilResponse>.Fail("Dados inválidos"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AtualizarPerfil(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarPerfil_DeveRetornar401_QuandoTokenInvalido()
    {
        // Arrange
        var fixture = new AtualizarPerfilControllerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AtualizarPerfilCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<AtualizarPerfilResponse>.Unauthorized("Token inválido"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AtualizarPerfil(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarPerfil_DeveChamarMediatorComCommand()
    {
        // Arrange
        var fixture = new AtualizarPerfilControllerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();
        var response = AtualizarPerfilResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AtualizarPerfilCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<AtualizarPerfilResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.AtualizarPerfil(command, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<AtualizarPerfilCommand>(), Arg.Any<CancellationToken>());
    }
}
