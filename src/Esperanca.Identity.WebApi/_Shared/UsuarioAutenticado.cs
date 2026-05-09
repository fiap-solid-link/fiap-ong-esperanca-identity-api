using System.Security.Claims;
using Esperanca.Identity.Application._Shared;

namespace Esperanca.Identity.WebApi._Shared;

public class UsuarioAutenticado(IHttpContextAccessor httpContextAccessor) : IUsuarioAutenticado
{
    public Guid? ObterUsuarioId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? user?.FindFirstValue("sub");

        return Guid.TryParse(userId, out var id) ? id : null;
    }
}
