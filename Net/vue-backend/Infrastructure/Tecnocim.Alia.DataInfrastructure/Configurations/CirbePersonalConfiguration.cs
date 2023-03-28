using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class CirbePersonalConfiguration : IEntityTypeConfiguration<CirbePersonal>
{
    public void Configure(EntityTypeBuilder<CirbePersonal> builder)
    {
        builder.HasKey(c => c.CirbePersonalId).HasName("PK_CirbePersonal");
        builder.Property(c => c.CirbePersonalId).UseIdentityColumn(1).ValueGeneratedOnAdd();

        builder.Property(c => c.CirbeId).IsRequired();
        builder.Property(c => c.EquivalenciasPersonalId).IsRequired();

        builder.HasOne(d => d.Cirbe)
                .WithMany(p => p.CirbePersonales)
                .HasForeignKey(d => d.CirbeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.EquivalenciasPersonal)
                 .WithMany(p => p.CirbePersonales)
                 .HasForeignKey(d => d.EquivalenciasPersonalId)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.NoAction);
    }
}
