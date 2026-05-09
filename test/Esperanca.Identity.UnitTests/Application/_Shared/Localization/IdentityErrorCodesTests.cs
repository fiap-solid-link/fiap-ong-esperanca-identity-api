using Esperanca.Identity.Application._Shared.Localization;

namespace Esperanca.Identity.UnitTests.Application._Shared.Localization;

public sealed class IdentityErrorCodesTests
{
    [Theory]
    [InlineData(IdentityErrorCodes.EmailOuSenhaInvalidos, "Identity:001")]
    [InlineData(IdentityErrorCodes.TokenInvalido, "Identity:002")]
    [InlineData(IdentityErrorCodes.RefreshTokenInvalidoOuExpirado, "Identity:003")]
    [InlineData(IdentityErrorCodes.EmailJaCadastrado, "Identity:004")]
    [InlineData(IdentityErrorCodes.UsuarioNaoEncontrado, "Identity:005")]
    [InlineData(IdentityErrorCodes.UsuarioJaPossuiGestor, "Identity:006")]
    [InlineData(IdentityErrorCodes.UsuarioNaoPossuiGestor, "Identity:007")]
    [InlineData(IdentityErrorCodes.RoleGestorNaoEncontrada, "Identity:008")]
    [InlineData(IdentityErrorCodes.NomeObrigatorio, "Identity:100")]
    [InlineData(IdentityErrorCodes.NomeMaximo150, "Identity:101")]
    [InlineData(IdentityErrorCodes.EmailObrigatorio, "Identity:102")]
    [InlineData(IdentityErrorCodes.EmailInvalido, "Identity:103")]
    [InlineData(IdentityErrorCodes.SenhaObrigatoria, "Identity:104")]
    [InlineData(IdentityErrorCodes.SenhaMinimo8, "Identity:105")]
    [InlineData(IdentityErrorCodes.ApelidoMaximo100, "Identity:106")]
    [InlineData(IdentityErrorCodes.RefreshTokenObrigatorio, "Identity:107")]
    [InlineData(IdentityErrorCodes.ErroDeValidacao, "Identity:900")]
    public void Codigo_DeveConterValorEsperado(string codigo, string esperado)
    {
        Assert.Equal(esperado, codigo);
    }

    [Theory]
    [InlineData(IdentityErrorCodes.EmailOuSenhaInvalidos)]
    [InlineData(IdentityErrorCodes.TokenInvalido)]
    [InlineData(IdentityErrorCodes.RefreshTokenInvalidoOuExpirado)]
    [InlineData(IdentityErrorCodes.EmailJaCadastrado)]
    [InlineData(IdentityErrorCodes.UsuarioNaoEncontrado)]
    [InlineData(IdentityErrorCodes.UsuarioJaPossuiGestor)]
    [InlineData(IdentityErrorCodes.UsuarioNaoPossuiGestor)]
    [InlineData(IdentityErrorCodes.RoleGestorNaoEncontrada)]
    [InlineData(IdentityErrorCodes.NomeObrigatorio)]
    [InlineData(IdentityErrorCodes.NomeMaximo150)]
    [InlineData(IdentityErrorCodes.EmailObrigatorio)]
    [InlineData(IdentityErrorCodes.EmailInvalido)]
    [InlineData(IdentityErrorCodes.SenhaObrigatoria)]
    [InlineData(IdentityErrorCodes.SenhaMinimo8)]
    [InlineData(IdentityErrorCodes.ApelidoMaximo100)]
    [InlineData(IdentityErrorCodes.RefreshTokenObrigatorio)]
    [InlineData(IdentityErrorCodes.ErroDeValidacao)]
    public void Codigo_DeveConterPrefixoIdentity(string codigo)
    {
        Assert.StartsWith("Identity:", codigo);
    }

    [Fact]
    public void TodosOsCodigos_DevemSerUnicos()
    {
        var codigos = new[]
        {
            IdentityErrorCodes.EmailOuSenhaInvalidos,
            IdentityErrorCodes.TokenInvalido,
            IdentityErrorCodes.RefreshTokenInvalidoOuExpirado,
            IdentityErrorCodes.EmailJaCadastrado,
            IdentityErrorCodes.UsuarioNaoEncontrado,
            IdentityErrorCodes.UsuarioJaPossuiGestor,
            IdentityErrorCodes.UsuarioNaoPossuiGestor,
            IdentityErrorCodes.RoleGestorNaoEncontrada,
            IdentityErrorCodes.NomeObrigatorio,
            IdentityErrorCodes.NomeMaximo150,
            IdentityErrorCodes.EmailObrigatorio,
            IdentityErrorCodes.EmailInvalido,
            IdentityErrorCodes.SenhaObrigatoria,
            IdentityErrorCodes.SenhaMinimo8,
            IdentityErrorCodes.ApelidoMaximo100,
            IdentityErrorCodes.RefreshTokenObrigatorio,
            IdentityErrorCodes.ErroDeValidacao
        };

        Assert.Equal(codigos.Length, codigos.Distinct().Count());
    }
}
