using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasNatintervConfiguration : IEntityTypeConfiguration<EquivalenciasNatinterv>
{
    public void Configure(EntityTypeBuilder<EquivalenciasNatinterv> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasNatinterv");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(3).IsRequired();
        builder.Property(c => c.Descripcion).HasMaxLength(50).IsRequired(false);
    }
}
