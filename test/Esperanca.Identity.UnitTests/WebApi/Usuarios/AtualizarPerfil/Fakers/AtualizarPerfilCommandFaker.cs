using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil.Fakers;

public static class AtualizarPerfilCommandFaker
{
    public static AtualizarPerfilCommand Valid(
        string nome = "João Atualizado",
        string? apelido = "joao123")
    {
        return new AtualizarPerfilCommand(nome, apelido);
    }

    public static AtualizarPerfilCommand Invalid()
    {
        return new AtualizarPerfilCommand("", null);
    }
}
