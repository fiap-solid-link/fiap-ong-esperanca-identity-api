using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Fakers;

public static class RoleFaker
{
    public static Role Doador()
    {
        return new Role(RoleTipo.Doador);
    }

    public static Role Admin()
    {
        return new Role(RoleTipo.Admin);
    }

    public static Role GestorONG()
    {
        return new Role(RoleTipo.GestorONG);
    }
}
