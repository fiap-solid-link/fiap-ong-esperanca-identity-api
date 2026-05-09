using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Mocks;

public sealed class UsuarioRepositoryMock
{
    public IUsuarioRepository Instance { get; } = Substitute.For<IUsuarioRepository>();

    public void SetupObterPorIdAsync(Guid id, Usuario? usuario)
    {
        Instance.ObterPorIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(usuario);
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
