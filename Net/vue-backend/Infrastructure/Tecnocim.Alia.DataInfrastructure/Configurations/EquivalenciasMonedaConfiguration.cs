using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasMonedaConfiguration : IEntityTypeConfiguration<EquivalenciasMoneda>
{
    public void Configure(EntityTypeBuilder<EquivalenciasMoneda> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasMoneda");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(3).IsRequired();
    }
}
