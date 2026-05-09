using Esperanca.Identity.Application.Autenticacao.Login;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Fixtures;

public sealed class LoginHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public RefreshTokenRepositoryMock RefreshTokenRepositoryMock { get; } = new();
    public PasswordHasherMock PasswordHasherMock { get; } = new();
    public JwtServiceMock JwtServiceMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public LoginHandler CriarHandler()
    {
        return new LoginHandler(
            UsuarioRepositoryMock.Instance,
            RefreshTokenRepositoryMock.Instance,
            PasswordHasherMock.Instance,
            JwtServiceMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
