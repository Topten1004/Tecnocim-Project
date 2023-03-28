using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class AnaliticaConfiguration : IEntityTypeConfiguration<Analitica>
{
    public void Configure(EntityTypeBuilder<Analitica> builder)
    {
        builder.HasKey(c => c.AnaliticaId).HasName("PK_Analitica");
        builder.Property(c => c.AnaliticaId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Cuenta).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Magnitud).HasPrecision(26,18).IsRequired(false);

        builder.Property(c => c.DocumentoId).IsRequired(true);

        builder.HasOne(d => d.Documento)
                .WithMany(p => p.Analiticas)
                .HasForeignKey(d => d.DocumentoId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
