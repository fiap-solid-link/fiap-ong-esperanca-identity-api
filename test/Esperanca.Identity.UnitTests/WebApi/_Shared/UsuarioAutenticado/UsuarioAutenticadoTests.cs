using System.Security.Claims;
using Esperanca.Identity.UnitTests.WebApi._Shared.UsuarioAutenticado.Fixtures;

namespace Esperanca.Identity.UnitTests.WebApi._Shared.UsuarioAutenticado;

public sealed class UsuarioAutenticadoTests
{
    [Fact]
    public void ObterUsuarioId_DeveRetornarId_QuandoClaimNameIdentifierPresente()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        var expectedId = Guid.NewGuid();
        fixture.HttpContextAccessorMock.SetupComClaim(ClaimTypes.NameIdentifier, expectedId.ToString());

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Equal(expectedId, result);
    }

    [Fact]
    public void ObterUsuarioId_DeveRetornarId_QuandoClaimSubPresente()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        var expectedId = Guid.NewGuid();
        fixture.HttpContextAccessorMock.SetupComClaim("sub", expectedId.ToString());

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Equal(expectedId, result);
    }

    [Fact]
    public void ObterUsuarioId_DevePriorizarNameIdentifier_QuandoAmbasClaimsPresentes()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        var nameIdentifierId = Guid.NewGuid();
        var subId = Guid.NewGuid();

        fixture.HttpContextAccessorMock.SetupComClaims(
            new Claim(ClaimTypes.NameIdentifier, nameIdentifierId.ToString()),
            new Claim("sub", subId.ToString()));

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Equal(nameIdentifierId, result);
    }

    [Fact]
    public void ObterUsuarioId_DeveRetornarNull_QuandoHttpContextNulo()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        fixture.HttpContextAccessorMock.SetupSemHttpContext();

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ObterUsuarioId_DeveRetornarNull_QuandoSemClaims()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        fixture.HttpContextAccessorMock.SetupSemClaims();

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ObterUsuarioId_DeveRetornarNull_QuandoClaimNaoEhGuidValido()
    {
        // Arrange
        var fixture = new UsuarioAutenticadoFixture();
        fixture.HttpContextAccessorMock.SetupComClaim(ClaimTypes.NameIdentifier, "valor-invalido");

        var sut = fixture.CriarUsuarioAutenticado();

        // Act
        var result = sut.ObterUsuarioId();

        // Assert
        Assert.Null(result);
    }
}
