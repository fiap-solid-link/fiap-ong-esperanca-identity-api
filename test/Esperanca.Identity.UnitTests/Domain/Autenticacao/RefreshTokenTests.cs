using Esperanca.Identity.Domain.Autenticacao;

namespace Esperanca.Identity.UnitTests.Domain.Autenticacao;

public sealed class RefreshTokenTests
{
    [Fact]
    public void Construtor_DeveDefinirPropriedadesCorretamente_EAtivoDeveSerTrue()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        const string token = "meu-refresh-token";
        var validade = TimeSpan.FromDays(7);

        // Act
        var refreshToken = new RefreshToken(usuarioId, token, validade);

        // Assert
        Assert.NotEqual(Guid.Empty, refreshToken.Id);
        Assert.Equal(usuarioId, refreshToken.UsuarioId);
        Assert.Equal(token, refreshToken.Token);
        Assert.True(refreshToken.CriadoEm <= DateTime.UtcNow);
        Assert.Equal(refreshToken.CriadoEm.Add(validade), refreshToken.ExpiraEm);
        Assert.Null(refreshToken.RevogadoEm);
        Assert.True(refreshToken.Ativo);
    }

    [Fact]
    public void Revogar_DeveDefinirRevogadoEm_EAtivoDeveRetornarFalse()
    {
        // Arrange
        var refreshToken = new RefreshToken(Guid.NewGuid(), "token", TimeSpan.FromDays(7));

        // Act
        refreshToken.Revogar();

        // Assert
        Assert.NotNull(refreshToken.RevogadoEm);
        Assert.False(refreshToken.Ativo);
    }
}
