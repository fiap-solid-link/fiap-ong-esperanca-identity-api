using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.UnitTests.Domain.Autenticacao;

public sealed class UsuarioTests
{
    [Fact]
    public void Construtor_DeveDefinirPropriedadesCorretamente()
    {
        // Arrange
        const string nome = "Maria Silva";
        const string email = "MARIA@Email.com";
        const string senhaHash = "hashed_123";

        // Act
        var usuario = new Usuario(nome, email, senhaHash);

        // Assert
        Assert.NotEqual(Guid.Empty, usuario.Id);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal("maria@email.com", usuario.Email);
        Assert.Equal(senhaHash, usuario.SenhaHash);
        Assert.Null(usuario.Apelido);
        Assert.True(usuario.CriadoEm <= DateTime.UtcNow);
        Assert.Null(usuario.AtualizadoEm);
        Assert.Empty(usuario.Roles);
        Assert.Empty(usuario.RefreshTokens);
    }

    [Fact]
    public void AtualizarPerfil_DeveAtualizarNomeEApelido()
    {
        // Arrange
        var usuario = new Usuario("Nome Original", "teste@email.com", "hash");
        const string novoNome = "Novo Nome";
        const string novoApelido = "Apelido";

        // Act
        usuario.AtualizarPerfil(novoNome, novoApelido);

        // Assert
        Assert.Equal(novoNome, usuario.Nome);
        Assert.Equal(novoApelido, usuario.Apelido);
        Assert.NotNull(usuario.AtualizadoEm);
    }

    [Fact]
    public void AdicionarRole_DeveAdicionarRoleNaLista()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        var role = new Role(RoleTipo.Doador);

        // Act
        usuario.AdicionarRole(role);

        // Assert
        Assert.Single(usuario.Roles);
        Assert.Contains(usuario.Roles, r => r.Tipo == RoleTipo.Doador);
        Assert.NotNull(usuario.AtualizadoEm);
    }

    [Fact]
    public void AdicionarRole_DeveIgnorarRoleDuplicada()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        var role1 = new Role(RoleTipo.Doador);
        var role2 = new Role(RoleTipo.Doador);

        // Act
        usuario.AdicionarRole(role1);
        usuario.AdicionarRole(role2);

        // Assert
        Assert.Single(usuario.Roles);
    }

    [Fact]
    public void RemoverRole_DeveRemoverRoleExistente()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        var role = new Role(RoleTipo.GestorONG);
        usuario.AdicionarRole(role);

        // Act
        usuario.RemoverRole(RoleTipo.GestorONG);

        // Assert
        Assert.Empty(usuario.Roles);
    }

    [Fact]
    public void RemoverRole_NaoDeveFalhar_QuandoRoleNaoExiste()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");

        // Act
        usuario.RemoverRole(RoleTipo.Admin);

        // Assert
        Assert.Empty(usuario.Roles);
    }

    [Fact]
    public void PossuiRole_DeveRetornarTrue_QuandoRoleExiste()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        usuario.AdicionarRole(new Role(RoleTipo.Doador));

        // Act
        var resultado = usuario.PossuiRole(RoleTipo.Doador);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void PossuiRole_DeveRetornarFalse_QuandoRoleNaoExiste()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");

        // Act
        var resultado = usuario.PossuiRole(RoleTipo.Admin);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void AdicionarRefreshToken_DeveAdicionarTokenNaLista()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        var refreshToken = new RefreshToken(usuario.Id, "token-value", TimeSpan.FromDays(7));

        // Act
        usuario.AdicionarRefreshToken(refreshToken);

        // Assert
        Assert.Single(usuario.RefreshTokens);
    }

    [Fact]
    public void RevogarTodosRefreshTokens_DeveRevogarTodosTokensAtivos()
    {
        // Arrange
        var usuario = new Usuario("Teste", "teste@email.com", "hash");
        var token1 = new RefreshToken(usuario.Id, "token-1", TimeSpan.FromDays(7));
        var token2 = new RefreshToken(usuario.Id, "token-2", TimeSpan.FromDays(7));
        usuario.AdicionarRefreshToken(token1);
        usuario.AdicionarRefreshToken(token2);

        // Act
        usuario.RevogarTodosRefreshTokens();

        // Assert
        Assert.All(usuario.RefreshTokens, t => Assert.False(t.Ativo));
    }
}
