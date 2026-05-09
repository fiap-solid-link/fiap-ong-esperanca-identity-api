using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.ObterMeuPerfil.Fakers;
using Esperanca.Identity.UnitTests.WebApi.Usuarios.ObterMeuPerfil.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ObterMeuPerfil;

public sealed class ObterMeuPerfilControllerTests
{
    [Fact]
    public async Task ObterMeuPerfil_DeveRetornarOk_QuandoResultadoComSucesso()
    {
        // Arrange
        var fixture = new ObterMeuPerfilControllerFixture();
        var response = ObterMeuPerfilResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<ObterMeuPerfilQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result<ObterMeuPerfilResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ObterMeuPerfil(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dados = Assert.IsType<ObterMeuPerfilResponse>(okResult.Value);
        Assert.Equal(response.Id, dados.Id);
        Assert.Equal(response.Nome, dados.Nome);
        Assert.Equal(response.Email, dados.Email);
        Assert.Equal(response.Apelido, dados.Apelido);
        Assert.Equal(response.Roles, dados.Roles);
    }

    [Fact]
    public async Task ObterMeuPerfil_DeveRetornar401_QuandoTokenInvalido()
    {
        // Arrange
        var fixture = new ObterMeuPerfilControllerFixture();

        fixture.Mediator
            .Send(Arg.Any<ObterMeuPerfilQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result<ObterMeuPerfilResponse>.Unauthorized("Token inválido"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ObterMeuPerfil(CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(401, objectResult.StatusCode);
    }

    [Fact]
    public async Task ObterMeuPerfil_DeveRetornar404_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new ObterMeuPerfilControllerFixture();

        fixture.Mediator
            .Send(Arg.Any<ObterMeuPerfilQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result<ObterMeuPerfilResponse>.NotFound("Usuário não encontrado"));

        var controller = fixture.CriarController();

        // Act
        var actionResult = await controller.ObterMeuPerfil(CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public async Task ObterMeuPerfil_DeveChamarMediatorComQuery()
    {
        // Arrange
        var fixture = new ObterMeuPerfilControllerFixture();
        var response = ObterMeuPerfilResponseFaker.Valid();

        fixture.Mediator
            .Send(Arg.Any<ObterMeuPerfilQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result<ObterMeuPerfilResponse>.Ok(response));

        var controller = fixture.CriarController();

        // Act
        await controller.ObterMeuPerfil(CancellationToken.None);

        // Assert
        await fixture.Mediator.Received(1).Send(Arg.Any<ObterMeuPerfilQuery>(), Arg.Any<CancellationToken>());
    }
}
