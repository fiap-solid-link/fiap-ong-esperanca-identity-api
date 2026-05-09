namespace Esperanca.Identity.Application._Shared;

public interface IPasswordHasher
{
    string Hash(string senha);
    bool Verificar(string senha, string hash);
}
