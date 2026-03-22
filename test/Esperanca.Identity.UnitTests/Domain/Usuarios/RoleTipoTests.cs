using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.UnitTests.Domain.Usuarios;

public sealed class RoleTipoTests
{
    [Fact]
    public void RoleTipo_DeveConterExatamenteTresValores()
    {
        // Act
        var valores = Enum.GetValues<RoleTipo>();

        // Assert
        Assert.Equal(3, valores.Length);
    }

    [Theory]
    [InlineData(RoleTipo.Admin, 1)]
    [InlineData(RoleTipo.GestorONG, 2)]
    [InlineData(RoleTipo.Doador, 3)]
    public void RoleTipo_DeveConterValorNumericoCorreto(RoleTipo tipo, int valorEsperado)
    {
        // Assert
        Assert.Equal(valorEsperado, (int)tipo);
    }

    [Theory]
    [InlineData(RoleTipo.Admin, "Admin")]
    [InlineData(RoleTipo.GestorONG, "GestorONG")]
    [InlineData(RoleTipo.Doador, "Doador")]
    public void RoleTipo_DeveRetornarNomeCorreto(RoleTipo tipo, string nomeEsperado)
    {
        // Assert
        Assert.Equal(nomeEsperado, tipo.ToString());
    }

    [Theory]
    [InlineData("Admin", RoleTipo.Admin)]
    [InlineData("GestorONG", RoleTipo.GestorONG)]
    [InlineData("Doador", RoleTipo.Doador)]
    public void RoleTipo_DeveSerConvertidoAPartirDeString(string nome, RoleTipo esperado)
    {
        // Act
        var parsed = Enum.Parse<RoleTipo>(nome);

        // Assert
        Assert.Equal(esperado, parsed);
    }

    [Fact]
    public void RoleTipo_DeveSerDistintoEntreOsValores()
    {
        // Assert
        Assert.NotEqual(RoleTipo.Admin, RoleTipo.GestorONG);
        Assert.NotEqual(RoleTipo.Admin, RoleTipo.Doador);
        Assert.NotEqual(RoleTipo.GestorONG, RoleTipo.Doador);
    }
}
