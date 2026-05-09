using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Autenticacao.Login;
using Esperanca.Identity.Application.Autenticacao.RenovarToken;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken;

public sealed class RenovarTokenControllerTests
{
    [Fact]
    public async Task RenovarToken_DeveRetornarOk_QuandoRefreshTokenValido()
    {
        // Arrange
        var fixture = new RenovarTokenControllerFixture();
        var command = RenovarTokenCommandFaker.Valid();
        var response = LoginResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RenovarTokenCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RenovarToken(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal(response.AccessToken, dados.AccessToken);
        Assert.Equal(response.RefreshToken, dados.RefreshToken);
        Assert.Equal(response.ExpiraEm, dados.ExpiraEm);
    }

    [Fact]
    public async Task RenovarToken_DeveRetornar401_QuandoRefreshTokenInvalido()
    {
        // Arrange
        var fixture = new RenovarTokenControllerFixture();
        var command = RenovarTokenCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RenovarTokenCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Unauthorized("Refresh token inválido"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RenovarToken(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task RenovarToken_DeveRetornar401_QuandoRefreshTokenExpirado()
    {
        // Arrange
        var fixture = new RenovarTokenControllerFixture();
        var command = RenovarTokenCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RenovarTokenCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Unauthorized("Refresh token expirado"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.RenovarToken(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task RenovarToken_DeveChamarMediatorComCommand()
    {
        // Arrange
        var fixture = new RenovarTokenControllerFixture();
        var command = RenovarTokenCommandFaker.Valid();
        var response = LoginResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<RenovarTokenCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<LoginResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.RenovarToken(command, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<RenovarTokenCommand>(), Arg.Any<CancellationToken>());
    }
}
