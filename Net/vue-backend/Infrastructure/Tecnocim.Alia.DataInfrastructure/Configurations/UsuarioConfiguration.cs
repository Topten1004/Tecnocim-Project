using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(c => c.UsuarioId).HasName("PK_Usuario");
        builder.Property(c => c.UsuarioId).HasColumnName("UsuarioId").UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Nombre).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Apellidos).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.Password).HasMaxLength(60);
        builder.Property(c => c.PuestoTrabajo).HasMaxLength(100).IsRequired(false);

        //builder.OwnsMany<RefreshToken>("RefreshTokens");

        builder.HasIndex(e => e.RolId, "IX_Usuarios_RolId");
        builder.HasOne(x => x.Rol).WithMany(x => x.Usuarios).HasForeignKey(x => x.RolId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(d => d.Empresas)
                   .WithMany(p => p.Usuarios)
                   .UsingEntity<Dictionary<string, object>>(
                       "UsuarioEmpresa",
                       l => l.HasOne<Empresa>().WithMany().HasForeignKey("EmpresaId"),
                       r => r.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"),
                       j =>
                       {
                           j.HasKey("UsuarioId", "EmpresaId").HasName("PK_UsuarioEmpresa");

                           j.ToTable("UsuarioEmpresas");

                           j.HasIndex(new[] { "EmpresaId" }, "IX_UsuarioEmpresas_EmpresaId");
                       });
    }
}
