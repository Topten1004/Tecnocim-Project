using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasEntidadConfiguration : IEntityTypeConfiguration<EquivalenciasEntidad>
{
    public void Configure(EntityTypeBuilder<EquivalenciasEntidad> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasEntidad");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Codigo).HasMaxLength(5).IsRequired();
        builder.Property(c => c.Nombre).HasMaxLength(100).IsRequired(false);
    }
}
