using Esperanca.Identity.Application.Autenticacao.RenovarToken;
using Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.RenovarToken.Fixtures;

public sealed class RenovarTokenHandlerFixture
{
    public RefreshTokenRepositoryMock RefreshTokenRepositoryMock { get; } = new();
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public JwtServiceMock JwtServiceMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public RenovarTokenHandler CriarHandler()
    {
        return new RenovarTokenHandler(
            RefreshTokenRepositoryMock.Instance,
            UsuarioRepositoryMock.Instance,
            JwtServiceMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
