using Esperanca.Identity.Application._Shared.Results;

namespace Esperanca.Identity.UnitTests.Application._Shared.Results;

public sealed class ResultTests
{
    [Fact]
    public void Ok_DeveRetornarSucesso_ComStatus200()
    {
        // Act
        var result = Result<string>.Ok("dados");

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("dados", result.Dados);
        Assert.Null(result.Erro);
    }

    [Fact]
    public void Created_DeveRetornarSucesso_ComStatus201()
    {
        // Act
        var result = Result<string>.Created("novo-recurso");

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal("novo-recurso", result.Dados);
        Assert.Null(result.Erro);
    }

    [Fact]
    public void Fail_DeveRetornarErro_ComStatus400PorPadrao()
    {
        // Act
        var result = Result<string>.Fail("erro genérico");

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("erro genérico", result.Erro);
        Assert.Null(result.Dados);
    }

    [Fact]
    public void Fail_DeveRetornarErro_ComStatusCustomizado()
    {
        // Act
        var result = Result<string>.Fail("conflito", 409);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(409, result.StatusCode);
        Assert.Equal("conflito", result.Erro);
        Assert.Null(result.Dados);
    }

    [Fact]
    public void NotFound_DeveRetornarErro_ComStatus404()
    {
        // Act
        var result = Result<string>.NotFound("não encontrado");

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal("não encontrado", result.Erro);
        Assert.Null(result.Dados);
    }

    [Fact]
    public void Unauthorized_DeveRetornarErro_ComStatus401()
    {
        // Act
        var result = Result<string>.Unauthorized("não autorizado");

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal("não autorizado", result.Erro);
        Assert.Null(result.Dados);
    }

    [Fact]
    public void Ok_DeveAceitarTipoComplexo()
    {
        // Arrange
        var dados = new { Id = Guid.NewGuid(), Nome = "Teste" };

        // Act
        var result = Result<object>.Ok(dados);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(dados, result.Dados);
    }
}
