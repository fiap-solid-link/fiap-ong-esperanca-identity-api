using Esperanca.Identity.Domain.Autenticacao;

namespace Esperanca.Identity.Application._Shared;

public interface IJwtService
{
    string GerarAccessToken(Usuario usuario);
    string GerarRefreshToken();
}
