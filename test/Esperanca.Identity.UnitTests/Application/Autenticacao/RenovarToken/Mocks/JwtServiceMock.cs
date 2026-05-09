using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Domain.Autenticacao;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Mocks;

public sealed class JwtServiceMock
{
    public IJwtService Instance { get; } = Substitute.For<IJwtService>();

    public void SetupGerarAccessToken(string accessToken)
    {
        Instance.GerarAccessToken(Arg.Any<Usuario>()).Returns(accessToken);
    }

    public void SetupGerarRefreshToken(string refreshToken)
    {
        Instance.GerarRefreshToken().Returns(refreshToken);
    }
}
