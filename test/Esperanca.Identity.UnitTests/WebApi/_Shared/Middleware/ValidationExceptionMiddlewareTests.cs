using System.Text.Json;
using Esperanca.Identity.UnitTests.WebApi._Shared.Middleware.Fixtures;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Esperanca.Identity.UnitTests.WebApi._Shared.Middleware;

public sealed class ValidationExceptionMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_DevePassarAdiante_QuandoNaoHouverExcecao()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var chamado = false;
        RequestDelegate next = _ => { chamado = true; return Task.CompletedTask; };

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.True(chamado);
        Assert.Equal(200, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_DeveRetornar400_QuandoValidationExceptionLancada()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var failures = new[] { new ValidationFailure("Email", "Email inválido") };
        RequestDelegate next = _ => throw new ValidationException(failures);

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(400, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_DeveRetornarContentTypeJson_QuandoValidationExceptionLancada()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var failures = new[] { new ValidationFailure("Nome", "Nome obrigatório") };
        RequestDelegate next = _ => throw new ValidationException(failures);

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal("application/json", context.Response.ContentType);
    }

    [Fact]
    public async Task InvokeAsync_DeveRetornarTituloCorreto_QuandoValidationExceptionLancada()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var failures = new[] { new ValidationFailure("Email", "Email inválido") };
        RequestDelegate next = _ => throw new ValidationException(failures);

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        var body = await ValidationExceptionMiddlewareFixture.LerResponseBody(context);
        var json = JsonDocument.Parse(body);

        Assert.Equal("Identity:900", json.RootElement.GetProperty("titulo").GetString());
    }

    [Fact]
    public async Task InvokeAsync_DeveAgruparErrosPorPropriedade_QuandoMultiplosErrosNaMesmaPropriedade()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var failures = new[]
        {
            new ValidationFailure("Senha", "Senha muito curta"),
            new ValidationFailure("Senha", "Senha deve conter número")
        };
        RequestDelegate next = _ => throw new ValidationException(failures);

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        var body = await ValidationExceptionMiddlewareFixture.LerResponseBody(context);
        var json = JsonDocument.Parse(body);

        var erros = json.RootElement.GetProperty("erros");
        var senhaErros = erros.GetProperty("Senha");

        Assert.Equal(2, senhaErros.GetArrayLength());
        Assert.Equal("Senha muito curta", senhaErros[0].GetString());
        Assert.Equal("Senha deve conter número", senhaErros[1].GetString());
    }

    [Fact]
    public async Task InvokeAsync_DeveSepararErrosDePropriedadesDiferentes_QuandoMultiplasPropriedades()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        var failures = new[]
        {
            new ValidationFailure("Email", "Email inválido"),
            new ValidationFailure("Nome", "Nome obrigatório")
        };
        RequestDelegate next = _ => throw new ValidationException(failures);

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        var body = await ValidationExceptionMiddlewareFixture.LerResponseBody(context);
        var json = JsonDocument.Parse(body);

        var erros = json.RootElement.GetProperty("erros");
        Assert.Equal("Email inválido", erros.GetProperty("Email")[0].GetString());
        Assert.Equal("Nome obrigatório", erros.GetProperty("Nome")[0].GetString());
    }

    [Fact]
    public async Task InvokeAsync_NaoDeveCapturar_QuandoExcecaoNaoEhValidationException()
    {
        // Arrange
        var fixture = new ValidationExceptionMiddlewareFixture();
        RequestDelegate next = _ => throw new InvalidOperationException("Erro genérico");

        var middleware = fixture.CriarMiddleware(next);
        var context = fixture.CriarHttpContext();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => middleware.InvokeAsync(context));
    }
}
