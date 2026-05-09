using Esperanca.Identity.WebApi._Shared.Middleware;
using Microsoft.AspNetCore.Http;

namespace Esperanca.Identity.UnitTests.WebApi._Shared.Middleware.Fixtures;

public sealed class ValidationExceptionMiddlewareFixture
{
    public ValidationExceptionMiddleware CriarMiddleware(RequestDelegate next)
    {
        return new ValidationExceptionMiddleware(next);
    }

    public DefaultHttpContext CriarHttpContext()
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        return context;
    }

    public static async Task<string> LerResponseBody(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        return await reader.ReadToEndAsync();
    }
}
