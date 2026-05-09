using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor.Fakers;
using Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor;

public sealed class RevogarGestorHandlerTests
{
    [Fact]
    public async Task Handle_DeveRevogarRoleGestorONG_QuandoUsuarioPossui()
    {
        // Arrange
        var fixture = new RevogarGestorHandlerFixture();
        var usuario = UsuarioFaker.WithGestorRole();
        var command = RevogarGestorCommandFaker.Valid(usuario.Id);

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(usuario.Id, result.Dados.UsuarioId);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarNotFound_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new RevogarGestorHandlerFixture();
        var command = RevogarGestorCommandFaker.Valid();

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(command.UsuarioId, null);

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
    public async Task Handle_DeveRetornarErro_QuandoUsuarioNaoPossuiGestorONG()
    {
        // Arrange
        var fixture = new RevogarGestorHandlerFixture();
        var usuario = UsuarioFaker.WithoutGestorRole();
        var command = RevogarGestorCommandFaker.Valid(usuario.Id);

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioNaoPossuiGestor, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }
}
