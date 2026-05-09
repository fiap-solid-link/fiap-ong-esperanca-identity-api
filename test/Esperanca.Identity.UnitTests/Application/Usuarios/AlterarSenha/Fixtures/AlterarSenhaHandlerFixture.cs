using Esperanca.Identity.Application.Usuarios.AlterarSenha;
using Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Mocks;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Fixtures;

public sealed class AlterarSenhaHandlerFixture
{
    public UsuarioRepositoryMock UsuarioRepositoryMock { get; } = new();
    public UsuarioAutenticadoMock UsuarioAutenticadoMock { get; } = new();
    public PasswordHasherMock PasswordHasherMock { get; } = new();
    public AppDbContextMock AppDbContextMock { get; } = new();
    public AppLocalizerMock AppLocalizerMock { get; } = new();

    public AlterarSenhaHandler CriarHandler()
    {
        return new AlterarSenhaHandler(
            UsuarioRepositoryMock.Instance,
            UsuarioAutenticadoMock.Instance,
            PasswordHasherMock.Instance,
            AppDbContextMock.Instance,
            AppLocalizerMock.Instance);
    }
}
