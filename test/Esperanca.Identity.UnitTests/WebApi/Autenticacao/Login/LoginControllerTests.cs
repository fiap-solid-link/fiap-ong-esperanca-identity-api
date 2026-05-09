using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Autenticacao.Login;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login;

public sealed class LoginControllerTests
{
    [Fact]
    public async Task Login_DeveRetornarOk_QuandoCredenciaisValidas()
    {
        // Arrange
        var fixture = new LoginControllerFixture();
        var command = LoginCommandFaker.Valid();
        var response = LoginResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Login(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal(response.AccessToken, dados.AccessToken);
        Assert.Equal(response.RefreshToken, dados.RefreshToken);
        Assert.Equal(response.ExpiraEm, dados.ExpiraEm);
    }

    [Fact]
    public async Task Login_DeveRetornar401_QuandoCredenciaisInvalidas()
    {
        // Arrange
        var fixture = new LoginControllerFixture();
        var command = LoginCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Unauthorized("Credenciais inválidas"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Login(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task Login_DeveRetornar400_QuandoDadosInvalidos()
    {
        // Arrange
        var fixture = new LoginControllerFixture();
        var command = LoginCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Fail("Dados inválidos"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.Login(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task Login_DeveChamarMediatorComCommand()
    {
        // Arrange
        var fixture = new LoginControllerFixture();
        var command = LoginCommandFaker.Valid();
        var response = LoginResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.Login(command, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>());
    }
}
