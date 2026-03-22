using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Esperanca.Identity.Infrastructure._Shared;

public static class DatabaseSeed
{
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityDbContext>>();

        await context.Database.MigrateAsync();

        if (await context.Usuarios.AnyAsync(u => u.Roles.Any(r => r.Tipo == RoleTipo.Admin)))
        {
            logger.LogInformation("Admin já existe. Seed ignorado.");
            return;
        }

        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Tipo == RoleTipo.Admin);
        if (adminRole is null) return;

        var admin = new Usuario("Administrador", "admin@esperanca.org", passwordHasher.Hash("Admin@123"));
        admin.AdicionarRole(adminRole);

        await context.Usuarios.AddAsync(admin);
        await context.SaveChangesAsync();

        logger.LogInformation("Admin seed criado com sucesso: admin@esperanca.org");
    }
}
