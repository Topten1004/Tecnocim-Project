using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.DataInfrastructure.Comparers;
using Tecnocim.Alia.DataInfrastructure.Converters;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class FicheroConfiguration : IEntityTypeConfiguration<Fichero>
{
    public void Configure(EntityTypeBuilder<Fichero> builder)
    {
        builder.HasKey(c => c.FicheroId).HasName("PK_Fichero");
        builder.Property(c => c.FicheroId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Origen).HasMaxLength(9).IsRequired();
        builder.Property(c => c.FechaContenido).HasConversion<DateOnlyConverter, DateOnlyComparer>().IsRequired();
        builder.Property(c => c.Estado).HasMaxLength(20).IsRequired(false);
        builder.Property(c => c.ExtractorId).IsRequired(false);
        builder.Property(c => c.EmpresaId).IsRequired(true);
        builder.Property(c => c.UsuarioId).IsRequired(true);

        builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Ficheros)
                .HasForeignKey(d => d.EmpresaId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.Usuario)
               .WithMany(p => p.Ficheros)
               .HasForeignKey(d => d.UsuarioId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
