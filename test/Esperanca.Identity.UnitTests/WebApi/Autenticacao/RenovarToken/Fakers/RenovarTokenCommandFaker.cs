using Esperanca.Identity.Application.Autenticacao.RenovarToken;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken.Fakers;

public static class RenovarTokenCommandFaker
{
    public static RenovarTokenCommand Valid(
        string refreshToken = "refresh-token-valido")
    {
        return new RenovarTokenCommand(refreshToken);
    }
}
