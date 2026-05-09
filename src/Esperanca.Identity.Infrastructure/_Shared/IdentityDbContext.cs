using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Domain.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace Esperanca.Identity.Infrastructure._Shared;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
