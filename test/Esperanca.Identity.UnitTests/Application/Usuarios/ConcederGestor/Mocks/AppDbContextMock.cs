using Esperanca.Identity.Application._Shared;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Mocks;

public sealed class AppDbContextMock
{
    public IAppDbContext Instance { get; } = Substitute.For<IAppDbContext>();

    public void VerifySaveChangesAsyncChamado()
    {
        Instance.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    public void VerifySaveChangesAsyncNaoChamado()
    {
        Instance.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
