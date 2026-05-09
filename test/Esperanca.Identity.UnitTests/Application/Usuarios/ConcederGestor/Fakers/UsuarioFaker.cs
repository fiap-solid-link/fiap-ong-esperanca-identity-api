using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Fakers;

public static class UsuarioFaker
{
    public static Usuario Valid(
        string nome = "João Silva",
        string email = "joao@email.com",
        string senhaHash = "hashed_password_123")
    {
        return new Usuario(nome, email, senhaHash);
    }

    public static Usuario ComRole(Role role)
    {
        var usuario = Valid();
        usuario.AdicionarRole(role);
        return usuario;
    }

    public static Usuario WithGestorRole()
    {
        var usuario = Valid();
        usuario.AdicionarRole(new Role(RoleTipo.GestorONG));
        return usuario;
    }

    public static Usuario WithoutGestorRole()
    {
        var usuario = Valid();
        usuario.AdicionarRole(new Role(RoleTipo.Doador));
        return usuario;
    }
}
