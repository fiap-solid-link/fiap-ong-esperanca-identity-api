using Esperanca.Identity.Application._Shared;

namespace Esperanca.Identity.Infrastructure._Shared;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha);

    public bool Verificar(string senha, string hash)
        => BCrypt.Net.BCrypt.Verify(senha, hash);
}
