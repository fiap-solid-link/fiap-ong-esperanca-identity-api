using Esperanca.Identity.Application.Autenticacao.Login;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login.Fakers;

public static class LoginCommandFaker
{
    public static LoginCommand Valid(
        string email = "joao@email.com",
        string senha = "Senha@123")
    {
        return new LoginCommand(email, senha);
    }
}
