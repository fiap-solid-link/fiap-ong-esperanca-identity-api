using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil.Fakers;
using Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil;

public sealed class AtualizarPerfilHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarNomeEApelido_QuandoDadosValidos()
    {
        // Arrange
        var fixture = new AtualizarPerfilHandlerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();
        var usuario = UsuarioFaker.Valid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuario.Id);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(usuario.Id, result.Dados.Id);
        Assert.Equal(command.Nome, result.Dados.Nome);
        Assert.Equal(command.Apelido, result.Dados.Apelido);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarUnauthorized_QuandoTokenInvalido()
    {
        // Arrange
        var fixture = new AtualizarPerfilHandlerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.TokenInvalido, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarNotFound_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new AtualizarPerfilHandlerFixture();
        var command = AtualizarPerfilCommandFaker.Valid();
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
}
