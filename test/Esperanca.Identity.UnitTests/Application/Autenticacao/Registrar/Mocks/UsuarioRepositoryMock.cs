using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Mocks;

public sealed class UsuarioRepositoryMock
{
    public IUsuarioRepository Instance { get; } = Substitute.For<IUsuarioRepository>();

    public void SetupEmailExisteAsync(string email, bool resultado)
    {
        Instance.EmailExisteAsync(email, Arg.Any<CancellationToken>())
            .Returns(resultado);
    }

    public void SetupObterPorEmailAsync(string email, Usuario? usuario)
    {
        Instance.ObterPorEmailAsync(email, Arg.Any<CancellationToken>())
            .Returns(usuario);
    }

    public void SetupObterPorIdAsync(Guid id, Usuario? usuario)
    {
        Instance.ObterPorIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(usuario);
    }

    public void VerifyAdicionarAsyncChamado()
    {
        Instance.Received(1).AdicionarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>());
    }

    public void VerifyAdicionarAsyncNaoChamado()
    {
        Instance.DidNotReceive().AdicionarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>());
    }

    public void VerifyAtualizarAsyncChamado()
    {
        Instance.Received(1).AtualizarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>());
    }

    public void VerifyAtualizarAsyncNaoChamado()
    {
        Instance.DidNotReceive().AtualizarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>());
    }
}
