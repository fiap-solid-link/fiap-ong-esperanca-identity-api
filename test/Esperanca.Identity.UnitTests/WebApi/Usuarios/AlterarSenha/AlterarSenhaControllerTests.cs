using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Usuarios.AlterarSenha;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.AlterarSenha.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.AlterarSenha.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AlterarSenha;

public sealed class AlterarSenhaControllerTests
{
    [Fact]
    public async Task AlterarSenha_DeveRetornarOk_QuandoResultadoComSucesso()
    {
        // Arrange
        var fixture = new AlterarSenhaControllerFixture();
        var command = AlterarSenhaCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AlterarSenhaCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<object>.Ok(new { }));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AlterarSenha(command, CancellationToken.None);

        // Assert
        Assert.IsType<OkResult>(actionResult);
    }

    [Fact]
    public async Task AlterarSenha_DeveRetornar400_QuandoSenhaAtualInvalida()
    {
        // Arrange
        var fixture = new AlterarSenhaControllerFixture();
        var command = AlterarSenhaCommandFaker.Invalid();

        fixture.Mediator
            .Send(Arg.Any<AlterarSenhaCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<object>.Fail("Senha atual inválida"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AlterarSenha(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, objectResult.StatusCode);
    }

    [Fact]
    public async Task AlterarSenha_DeveRetornar401_QuandoTokenInvalido()
    {
        // Arrange
        var fixture = new AlterarSenhaControllerFixture();
        var command = AlterarSenhaCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AlterarSenhaCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<object>.Unauthorized("Token inválido"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.AlterarSenha(command, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task AlterarSenha_DeveChamarMediatorComCommand()
    {
        // Arrange
        var fixture = new AlterarSenhaControllerFixture();
        var command = AlterarSenhaCommandFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<AlterarSenhaCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result<object>.Ok(new { }));

        var controller = fixture.CriarController();

        // Act
        await controller.AlterarSenha(command, CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<AlterarSenhaCommand>(), Arg.Any<CancellationToken>());
    }
}
