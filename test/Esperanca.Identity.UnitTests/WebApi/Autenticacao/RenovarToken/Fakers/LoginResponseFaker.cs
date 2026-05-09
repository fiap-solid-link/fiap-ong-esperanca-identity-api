using Esperanca.Identity.Application.Autenticacao.Login;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.RenovarToken.Fakers;

public static class LoginResponseFaker
{
    public static LoginResponse Valid(
        string accessToken = "novo-access-token",
        string refreshToken = "novo-refresh-token",
        DateTime? expiraEm = null)
    {
        return new LoginResponse(accessToken, refreshToken, expiraEm ?? DateTime.UtcNow.AddMinutes(30));
    }
}
