using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.UnitTests.Application.Autenticacao.ObterMeuPerfil.Fakers;
using Esperanca.Identity.UnitTests.Application.Autenticacao.ObterMeuPerfil.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.ObterMeuPerfil;

public sealed class ObterMeuPerfilHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarPerfil_QuandoUsuarioAutenticado()
    {
        // Arrange
        var fixture = new ObterMeuPerfilHandlerFixture();
        var query = ObterMeuPerfilQueryFaker.Valid();
        var roleDoador = RoleFaker.Doador();
        var usuario = UsuarioFaker.ComRole(roleDoador);

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuario.Id);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuario.Id, usuario);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(usuario.Id, result.Dados.Id);
        Assert.Equal(usuario.Nome, result.Dados.Nome);
        Assert.Equal(usuario.Email, result.Dados.Email);
        Assert.Equal(usuario.Apelido, result.Dados.Apelido);
        Assert.Contains(roleDoador.Nome, result.Dados.Roles);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoTokenInvalido()
    {
        // Arrange
        var fixture = new ObterMeuPerfilHandlerFixture();
        var query = ObterMeuPerfilQueryFaker.Valid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.TokenInvalido, result.Erro);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var fixture = new ObterMeuPerfilHandlerFixture();
        var query = ObterMeuPerfilQueryFaker.Valid();
        var usuarioId = Guid.NewGuid();

        fixture.UsuarioAutenticadoMock.SetupObterUsuarioId(usuarioId);
        fixture.UsuarioRepositoryMock.SetupObterPorIdAsync(usuarioId, null);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.UsuarioNaoEncontrado, result.Erro);
    }
}
