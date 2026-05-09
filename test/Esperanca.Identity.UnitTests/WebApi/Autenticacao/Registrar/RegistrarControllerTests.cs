using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Autenticacao.Registrar;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.Registrar.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.Registrar.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Registrar;

public sealed class RegistrarControllerTests
{
    [Fact]
    public async Task Registrar_DeveRetornar201_QuandoDadosValidos()
    {
        // Arrange
        var fixture = new RegistrarControllerFixture();
        var command = RegistrarCommandFaker.Valid();
        var response = RegistrarResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RegistrarCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RegistrarResponse>.Created(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Registrar(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(201, objectResult.StatusCode);
        var dados = Assert.IsType<RegistrarResponse>(objectResult.Value);
        Assert.Equal(response.Id, dados.Id);
        Assert.Equal(response.Nome, dados.Nome);
        Assert.Equal(response.Email, dados.Email);
    }

    [Fact]
    public async Task Registrar_DeveRetornar400_QuandoEmailJaCadastrado()
    {
        // Arrange
        var fixture = new RegistrarControllerFixture();
        var command = RegistrarCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RegistrarCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RegistrarResponse>.Fail("Email já cadastrado"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Registrar(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task Registrar_DeveRetornar400_QuandoDadosInvalidos()
    {
        // Arrange
        var fixture = new RegistrarControllerFixture();
        var command = RegistrarCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RegistrarCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RegistrarResponse>.Fail("Dados inválidos"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Registrar(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task Registrar_DeveChamarMediatorComCommand()
    {
        // Arrange
        var fixture = new RegistrarControllerFixture();
        var command = RegistrarCommandFaker.Valid();
        var response = RegistrarResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RegistrarCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<RegistrarResponse>.Created(response));

        var controller = fixture.CriarController();

        // Act
        await controller.Registrar(command, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<RegistrarCommand>(), Arg.Any<CancellationToken>());
    }
}
