using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios;
using Esperanca.Identity.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Esperanca.Identity.Infrastructure.Usuarios;

public class UsuarioRepository(IdentityDbContext context) : IUsuarioRepository
{
    public async Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        => await context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct = default)
        => await context.Usuarios
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), ct);

    public async Task<bool> EmailExisteAsync(string email, CancellationToken ct = default)
        => await context.Usuarios
            .AnyAsync(u => u.Email == email.ToLowerInvariant(), ct);

    public async Task AdicionarAsync(Usuario usuario, CancellationToken ct = default)
        => await context.Usuarios.AddAsync(usuario, ct);

    public Task AtualizarAsync(Usuario usuario, CancellationToken ct = default)
    {
        context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }
}
