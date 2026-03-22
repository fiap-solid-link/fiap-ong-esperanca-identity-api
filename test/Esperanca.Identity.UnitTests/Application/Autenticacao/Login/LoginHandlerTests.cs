using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Fakers;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Login;

public sealed class LoginHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarTokens_QuandoCredenciaisValidas()
    {
        // Arrange
        var fixture = new LoginHandlerFixture();
        var command = LoginCommandFaker.Valid();
        var usuario = UsuarioFaker.Valid(email: command.Email, senhaHash: "hashed_password");
        const string accessToken = "access-token-jwt";
        const string refreshToken = "refresh-token-value";

        fixture.UsuarioRepositoryMock.SetupObterPorEmailAsync(command.Email, usuario);
        fixture.PasswordHasherMock.SetupVerificar(command.Senha, usuario.SenhaHash, true);
        fixture.JwtServiceMock.SetupGerarAccessToken(accessToken);
        fixture.JwtServiceMock.SetupGerarRefreshToken(refreshToken);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(accessToken, result.Dados.AccessToken);
        Assert.Equal(refreshToken, result.Dados.RefreshToken);
        Assert.True(result.Dados.ExpiraEm > DateTime.UtcNow);
        fixture.RefreshTokenRepositoryMock.VerifyAdicionarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoEmailNaoEncontrado()
    {
        // Arrange
        var fixture = new LoginHandlerFixture();
        var command = LoginCommandFaker.Valid(email: "inexistente@email.com");

        fixture.UsuarioRepositoryMock.SetupObterPorEmailAsync(command.Email, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.EmailOuSenhaInvalidos, result.Erro);
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoSenhaIncorreta()
    {
        // Arrange
        var fixture = new LoginHandlerFixture();
        var command = LoginCommandFaker.Valid();
        var usuario = UsuarioFaker.Valid(email: command.Email, senhaHash: "hashed_password");

        fixture.UsuarioRepositoryMock.SetupObterPorEmailAsync(command.Email, usuario);
        fixture.PasswordHasherMock.SetupVerificar(command.Senha, usuario.SenhaHash, false);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.EmailOuSenhaInvalidos, result.Erro);
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }
}
