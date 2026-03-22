using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Domain.Usuarios.Enums;
using Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Fakers;
using Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor;

public sealed class ConcederGestorHandlerTests
{
    [Fact]
    public async Task Handle_DeveConcederRoleGestorONG_QuandoUsuarioNaoPossui()
    {
        // Arrange
        var fixture = new ConcederGestorHandlerFixture();
        var usuario = UsuarioFaker.WithoutGestorRole();
        var command = ConcederGestorCommandFaker.Valid(usuario.Id);
        var roleGestor = RoleFaker.GestorONG();

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);
        fixture.RoleRepositoryMock.SetupObterPorTipoAsync(RoleTipo.GestorONG, roleGestor);

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
        var fixture = new ConcederGestorHandlerFixture();
        var command = ConcederGestorCommandFaker.Valid();

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
    public async Task Handle_DeveRetornarErro_QuandoUsuarioJaPossuiGestorONG()
    {
        // Arrange
        var fixture = new ConcederGestorHandlerFixture();
        var usuario = UsuarioFaker.WithGestorRole();
        var command = ConcederGestorCommandFaker.Valid(usuario.Id);

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioJaPossuiGestor, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoRoleGestorNaoEncontradaNoBanco()
    {
        // Arrange
        var fixture = new ConcederGestorHandlerFixture();
        var usuario = UsuarioFaker.WithoutGestorRole();
        var command = ConcederGestorCommandFaker.Valid(usuario.Id);

        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);
        fixture.RoleRepositoryMock.SetupObterPorTipoAsync(RoleTipo.GestorONG, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.RoleGestorNaoEncontrada, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAtualizarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }
}
