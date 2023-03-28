using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasProductoConfiguration : IEntityTypeConfiguration<EquivalenciasProducto>
{
    public void Configure(EntityTypeBuilder<EquivalenciasProducto> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasProducto");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Descripcion).HasMaxLength(100).IsRequired(false);
        builder.Property(c => c.Subtipo).HasMaxLength(100).IsRequired(false);
    }
}
