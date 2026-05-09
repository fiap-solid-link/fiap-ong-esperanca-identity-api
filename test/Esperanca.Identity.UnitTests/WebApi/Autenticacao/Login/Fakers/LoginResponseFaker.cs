using Esperanca.Identity.Application.Autenticacao.Login;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Login.Fakers;

public static class LoginResponseFaker
{
    public static LoginResponse Valid(
        string accessToken = "access-token-valido",
        string refreshToken = "refresh-token-valido",
        DateTime? expiraEm = null)
    {
        return new LoginResponse(accessToken, refreshToken, expiraEm ?? DateTime.UtcNow.AddMinutes(30));
    }
}
