using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Fakers;
using Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha;

public sealed class AlterarSenhaHandlerTests
{
    [Fact]
    public async Task Handle_DeveAlterarSenha_QuandoDadosValidos()
    {
        // Arrange
        var fixture = new AlterarSenhaHandlerFixture();
        var command = AlterarSenhaCommandFaker.Valid();
        var usuario = UsuarioFaker.Valid(senhaHash: "hashed_senha_atual");
        const string novaSenhaHash = "hashed_nova_senha";

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuario.Id);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);
        fixture.PasswordHasherMock.SetupVerificar(command.SenhaAtual, usuario.SenhaHash, true);
        fixture.PasswordHasherMock.SetupHash(command.NovaSenha, novaSenhaHash);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(novaSenhaHash, usuario.SenhaHash);
        Assert.NotNull(usuario.AtualizadoEm);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarUnauthorized_QuandoUsuarioNaoAutenticado()
    {
        // Arrange
        var fixture = new AlterarSenhaHandlerFixture();
        var command = AlterarSenhaCommandFaker.Valid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioNaoEncontrado, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarNotFound_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new AlterarSenhaHandlerFixture();
        var command = AlterarSenhaCommandFaker.Valid();
        var usuarioId = Guid.NewGuid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuarioId);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuarioId, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioNaoEncontrado, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarFail_QuandoSenhaAtualInvalida()
    {
        // Arrange
        var fixture = new AlterarSenhaHandlerFixture();
        var command = AlterarSenhaCommandFaker.Valid(senhaAtual: "SenhaErrada@000");
        var usuario = UsuarioFaker.Valid(senhaHash: "hashed_senha_correta");

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuario.Id);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);
        fixture.PasswordHasherMock.SetupVerificar(command.SenhaAtual, usuario.SenhaHash, false);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.SenhaAtualInvalida, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }
}
