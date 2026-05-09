using Esperanca.Identity.Application.Usuarios.AlterarSenha;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AlterarSenha.Fakers;

public static class AlterarSenhaCommandFaker
{
    public static AlterarSenhaCommand Valid(
        string senhaAtual = "SenhaAtual@123",
        string novaSenha = "NovaSenha@456")
    {
        return new AlterarSenhaCommand(senhaAtual, novaSenha);
    }

    public static AlterarSenhaCommand Invalid()
    {
        return new AlterarSenhaCommand("", "");
    }
}
