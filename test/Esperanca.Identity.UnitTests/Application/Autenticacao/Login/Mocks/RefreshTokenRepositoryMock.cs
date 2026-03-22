using Esperanca.Identity.Domain.Autenticacao;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Mocks;

public sealed class RefreshTokenRepositoryMock
{
    public IRefreshTokenRepository Instance { get; } = Substitute.For<IRefreshTokenRepository>();

    public void SetupObterPorTokenAsync(string token, RefreshToken? refreshToken)
    {
        Instance.ObterPorTokenAsync(token, Arg.Any<CancellationToken>())
            .Returns(refreshToken);
    }

    public void VerifyAdicionarAsyncChamado()
    {
        Instance.Received(1).AdicionarAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>());
    }

    public void VerifyAtualizarAsyncChamado()
    {
        Instance.Received(1).AtualizarAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>());
    }
}
