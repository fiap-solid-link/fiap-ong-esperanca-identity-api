using Esperanca.Identity.Domain.Autenticacao;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Fakers;

public static class RefreshTokenFaker
{
    public static RefreshToken Valid(Guid? usuarioId = null, string token = "valid-refresh-token")
    {
        return new RefreshToken(
            usuarioId ?? Guid.NewGuid(),
            token,
            TimeSpan.FromDays(7));
    }

    public static RefreshToken Expirado(Guid? usuarioId = null, string token = "expired-refresh-token")
    {
        var refreshToken = new RefreshToken(
            usuarioId ?? Guid.NewGuid(),
            token,
            TimeSpan.FromDays(7));

        refreshToken.Revogar();
        return refreshToken;
    }
}
