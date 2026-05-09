using Esperanca.Identity.Application.Autenticacao.RenovarToken;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Fakers;

public static class RenovarTokenCommandFaker
{
    public static RenovarTokenCommand Valid(string refreshToken = "valid-refresh-token")
    {
        return new RenovarTokenCommand(refreshToken);
    }
}
