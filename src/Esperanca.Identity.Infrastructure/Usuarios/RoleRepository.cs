using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Domain.Usuarios.Enums;
using Esperanca.Identity.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Esperanca.Identity.Infrastructure.Usuarios;

public class RoleRepository(IdentityDbContext context) : IRoleRepository
{
    public async Task<Role?> ObterPorTipoAsync(RoleTipo tipo, CancellationToken ct = default)
        => await context.Roles.FirstOrDefaultAsync(r => r.Tipo == tipo, ct);

    public async Task<List<Role>> ObterTodosAsync(CancellationToken ct = default)
        => await context.Roles.ToListAsync(ct);
}
