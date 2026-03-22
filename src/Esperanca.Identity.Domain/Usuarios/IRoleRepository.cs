using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.Domain.Usuarios;

public interface IRoleRepository
{
    Task<Role?> ObterPorTipoAsync(RoleTipo tipo, CancellationToken ct = default);
    Task<List<Role>> ObterTodosAsync(CancellationToken ct = default);
}
