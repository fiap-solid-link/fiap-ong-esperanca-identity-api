namespace Esperanca.Identity.Application._Shared.Localization;

public static class IdentityErrorCodes
{
    // Autenticacao
    public const string EmailOuSenhaInvalidos         = "Identity:001";
    public const string TokenInvalido                 = "Identity:002";
    public const string RefreshTokenInvalidoOuExpirado = "Identity:003";
    public const string EmailJaCadastrado             = "Identity:004";

    // Usuarios
    public const string UsuarioNaoEncontrado          = "Identity:005";
    public const string UsuarioJaPossuiGestor         = "Identity:006";
    public const string UsuarioNaoPossuiGestor        = "Identity:007";
    public const string RoleGestorNaoEncontrada       = "Identity:008";

    // Validacao
    public const string NomeObrigatorio               = "Identity:100";
    public const string NomeMaximo150                 = "Identity:101";
    public const string EmailObrigatorio              = "Identity:102";
    public const string EmailInvalido                 = "Identity:103";
    public const string SenhaObrigatoria              = "Identity:104";
    public const string SenhaMinimo8                  = "Identity:105";
    public const string ApelidoMaximo100              = "Identity:106";
    public const string RefreshTokenObrigatorio       = "Identity:107";

    // Shared
    public const string ErroDeValidacao               = "Identity:900";
}
