using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class OperacionConfiguration : IEntityTypeConfiguration<Operacion>
{
    public void Configure(EntityTypeBuilder<Operacion> builder)
    {
        builder.HasKey(c => c.OperacionId).HasName("PK_Operacion");
        builder.Property(c => c.OperacionId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Momento).IsRequired();
        builder.Property(c => c.TextoOperacion).HasMaxLength(200).IsRequired(false);
        
        builder.Property(c => c.DocumentoId).IsRequired(false);
        builder.Property(c => c.EmpresaId).IsRequired(true);
        builder.Property(c => c.UsuarioId).IsRequired(true);

        builder.HasOne(d => d.Documento)
                .WithMany(p => p.Operaciones)
                .HasForeignKey(d => d.DocumentoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.Empresa)
               .WithMany(p => p.Operaciones)
               .HasForeignKey(d => d.EmpresaId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.Usuario)
               .WithMany(p => p.Operaciones)
               .HasForeignKey(d => d.UsuarioId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
