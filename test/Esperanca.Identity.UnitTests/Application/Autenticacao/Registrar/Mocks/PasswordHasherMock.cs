using Esperanca.Identity.Application._Shared;
using NSubstitute;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Mocks;

public sealed class PasswordHasherMock
{
    public IPasswordHasher Instance { get; } = Substitute.For<IPasswordHasher>();

    public void SetupHash(string senha, string hash)
    {
        Instance.Hash(senha).Returns(hash);
    }

    public void SetupVerificar(string senha, string hash, bool resultado)
    {
        Instance.Verificar(senha, hash).Returns(resultado);
    }
}
