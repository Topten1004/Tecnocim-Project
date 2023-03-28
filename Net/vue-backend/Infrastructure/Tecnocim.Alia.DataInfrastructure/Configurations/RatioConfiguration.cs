using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class RatioConfiguration : IEntityTypeConfiguration<Ratio>
{
    public void Configure(EntityTypeBuilder<Ratio> builder)
    {
        builder.HasKey(c => c.RatioId).HasName("PK_Ratio");
        builder.Property(c => c.RatioId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Concepto).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Magnitud).HasPrecision(26, 18).IsRequired();
        builder.Property(c => c.DocumentoId).IsRequired(true);

        builder.HasOne(d => d.Documento)
                .WithMany(p => p.Ratios)
                .HasForeignKey(d => d.DocumentoId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
