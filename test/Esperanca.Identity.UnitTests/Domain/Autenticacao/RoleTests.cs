using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.UnitTests.Domain.Autenticacao;

public sealed class RoleTests
{
    [Theory]
    [InlineData(RoleTipo.Admin)]
    [InlineData(RoleTipo.GestorONG)]
    [InlineData(RoleTipo.Doador)]
    public void Construtor_DeveDefinirTipoENomeAPartirDeRoleTipo(RoleTipo tipo)
    {
        // Act
        var role = new Role(tipo);

        // Assert
        Assert.NotEqual(Guid.Empty, role.Id);
        Assert.Equal(tipo, role.Tipo);
        Assert.Equal(tipo.ToString(), role.Nome);
        Assert.Empty(role.Usuarios);
    }
}
