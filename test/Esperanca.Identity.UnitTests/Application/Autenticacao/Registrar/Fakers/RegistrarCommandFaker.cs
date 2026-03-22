using Esperanca.Identity.Application.Autenticacao.Registrar;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Fakers;

public static class RegistrarCommandFaker
{
    public static RegistrarCommand Valid(
        string nome = "João Silva",
        string email = "joao@email.com",
        string senha = "Senha@123")
    {
        return new RegistrarCommand(nome, email, senha);
    }
}
