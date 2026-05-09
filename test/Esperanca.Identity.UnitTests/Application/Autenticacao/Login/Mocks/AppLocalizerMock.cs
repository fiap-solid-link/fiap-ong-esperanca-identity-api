using Esperanca.Identity.Application._Shared.Localization;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Login.Mocks;

public sealed class AppLocalizerMock
{
    public IAppLocalizer Instance { get; } = Substitute.For<IAppLocalizer>();

    public AppLocalizerMock()
    {
        Instance[Arg.Any<string>()].Returns(callInfo => callInfo.Arg<string>());
    }
}
