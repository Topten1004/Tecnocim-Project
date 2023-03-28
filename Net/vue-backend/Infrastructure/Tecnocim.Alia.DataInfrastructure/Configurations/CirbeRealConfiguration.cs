using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class CirbeRealConfiguration : IEntityTypeConfiguration<CirbeReal>
{
    public void Configure(EntityTypeBuilder<CirbeReal> builder)
    {
        builder.HasKey(c => c.CirbeRealId).HasName("PK_CirbeReal");
        builder.Property(c => c.CirbeRealId).UseIdentityColumn(1).ValueGeneratedOnAdd();

        builder.Property(c => c.CirbeId).IsRequired();
        builder.Property(c => c.EquivalenciasRealId).IsRequired();

        builder.HasOne(d => d.Cirbe)
                .WithMany(p => p.CirbeReales)
                .HasForeignKey(d => d.CirbeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.EquivalenciasReal)
                 .WithMany(p => p.CirbeReales)
                 .HasForeignKey(d => d.EquivalenciasRealId)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.NoAction);
    }
}