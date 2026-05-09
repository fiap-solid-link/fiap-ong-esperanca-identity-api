using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AtualizarPerfil.Fakers;

public static class AtualizarPerfilCommandFaker
{
    public static AtualizarPerfilCommand Valid(
        string nome = "Novo Nome",
        string? apelido = "Novo Apelido")
    {
        return new AtualizarPerfilCommand(nome, apelido);
    }
}
