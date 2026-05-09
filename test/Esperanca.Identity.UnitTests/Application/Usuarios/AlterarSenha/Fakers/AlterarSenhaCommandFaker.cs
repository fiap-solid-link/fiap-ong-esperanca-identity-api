using Esperanca.Identity.Application.Usuarios.AlterarSenha;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Fakers;

public static class AlterarSenhaCommandFaker
{
    public static AlterarSenhaCommand Valid(
        string senhaAtual = "SenhaAtual@123",
        string novaSenha = "NovaSenha@456")
    {
        return new AlterarSenhaCommand(senhaAtual, novaSenha);
    }
}
