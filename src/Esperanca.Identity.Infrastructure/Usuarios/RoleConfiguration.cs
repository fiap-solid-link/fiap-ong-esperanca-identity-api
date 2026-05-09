using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Domain.Usuarios.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Esperanca.Identity.Infrastructure.Usuarios;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("perfis");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Tipo)
            .IsRequired();

        builder.HasIndex(r => r.Tipo)
            .IsUnique();

        builder.HasData(
            new { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Tipo = RoleTipo.Admin, Nome = "Admin" },
            new { Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"), Tipo = RoleTipo.GestorONG, Nome = "GestorONG" },
            new { Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"), Tipo = RoleTipo.Doador, Nome = "Doador" }
        );
    }
}
