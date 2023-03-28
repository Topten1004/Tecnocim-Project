using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class ContabilidadConfiguration : IEntityTypeConfiguration<Contabilidad>
{
    public void Configure(EntityTypeBuilder<Contabilidad> builder)
    {
        builder.HasKey(c => c.ContabilidadId).HasName("PK_Contabilidad");
        builder.Property(c => c.ContabilidadId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Concepto).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Magnitud).HasPrecision(26, 18).IsRequired(false);
        builder.Property(c => c.Codigo).HasMaxLength(10).IsRequired(false);

        builder.Property(c => c.DocumentoId).IsRequired(true);

        builder.HasOne(d => d.Documento)
                .WithMany(p => p.Contabilidades)
                .HasForeignKey(d => d.DocumentoId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
