using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.WebApi._Shared.UsuarioAutenticado.Mocks;

public sealed class HttpContextAccessorMock
{
    public IHttpContextAccessor Instance { get; } = Substitute.For<IHttpContextAccessor>();

    public void SetupComClaim(string claimType, string claimValue)
    {
        var claims = new[] { new Claim(claimType, claimValue) };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = principal };
        Instance.HttpContext.Returns(httpContext);
    }

    public void SetupComClaims(params Claim[] claims)
    {
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = principal };
        Instance.HttpContext.Returns(httpContext);
    }

    public void SetupSemHttpContext()
    {
        Instance.HttpContext.Returns((HttpContext?)null);
    }

    public void SetupSemClaims()
    {
        var httpContext = new DefaultHttpContext();
        Instance.HttpContext.Returns(httpContext);
    }
}
