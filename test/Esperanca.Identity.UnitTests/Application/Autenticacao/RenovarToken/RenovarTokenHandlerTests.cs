using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Fakers;
using Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken;

public sealed class RenovarTokenHandlerTests
{
    [Fact]
    public async Task Handle_DeveRotacionarTokens_QuandoRefreshTokenValido()
    {
        // Arrange
        var fixture = new RenovarTokenHandlerFixture();
        var command = RenovarTokenCommandFaker.Valid();
        var usuario = UsuarioFaker.Valid();
        var refreshTokenExistente = RefreshTokenFaker.Valid(usuarioId: usuario.Id, token: command.RefreshToken);
        const string novoAccessToken = "novo-access-token";
        const string novoRefreshToken = "novo-refresh-token";

        fixture.RefreshTokenRepositoryMock.SetupObterPorTokenAsync(command.RefreshToken, refreshTokenExistente);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(refreshTokenExistente.UsuarioId, usuario);
        fixture.JwtServiceMock.SetupGerarAccessToken(novoAccessToken);
        fixture.JwtServiceMock.SetupGerarRefreshToken(novoRefreshToken);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(novoAccessToken, result.Dados.AccessToken);
        Assert.Equal(novoRefreshToken, result.Dados.RefreshToken);
        Assert.True(result.Dados.ExpiraEm > DateTime.UtcNow);
        fixture.RefreshTokenRepositoryMock.VerifyAtualizarAsyncChamado();
        fixture.RefreshTokenRepositoryMock.VerifyAdicionarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoRefreshTokenInvalidoOuExpirado()
    {
        // Arrange
        var fixture = new RenovarTokenHandlerFixture();
        var command = RenovarTokenCommandFaker.Valid(refreshToken: "token-invalido");

        fixture.RefreshTokenRepositoryMock.SetupObterPorTokenAsync(command.RefreshToken, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.RefreshTokenInvalidoOuExpirado, result.Erro);
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoRefreshTokenRevogado()
    {
        // Arrange
        var fixture = new RenovarTokenHandlerFixture();
        var command = RenovarTokenCommandFaker.Valid();
        var refreshTokenRevogado = RefreshTokenFaker.Expirado(token: command.RefreshToken);

        fixture.RefreshTokenRepositoryMock.SetupObterPorTokenAsync(command.RefreshToken, refreshTokenRevogado);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.RefreshTokenInvalidoOuExpirado, result.Erro);
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new RenovarTokenHandlerFixture();
        var command = RenovarTokenCommandFaker.Valid();
        var refreshTokenExistente = RefreshTokenFaker.Valid(token: command.RefreshToken);

        fixture.RefreshTokenRepositoryMock.SetupObterPorTokenAsync(command.RefreshToken, refreshTokenExistente);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(refreshTokenExistente.UsuarioId, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioNaoEncontrado, result.Erro);
    }
}
