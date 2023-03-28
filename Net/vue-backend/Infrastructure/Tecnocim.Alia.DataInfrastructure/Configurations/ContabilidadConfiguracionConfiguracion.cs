using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class ContabilidadConfiguracionConfiguracion : IEntityTypeConfiguration<ContabilidadConfiguracion>
{
    public void Configure(EntityTypeBuilder<ContabilidadConfiguracion> builder)
    {
        builder.HasKey(c => c.ContabilidadConfiguracionId).HasName("PK_ContabilidadConfiguracion");
        builder.Property(c => c.ContabilidadConfiguracionId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Concepto).HasMaxLength(100).IsRequired();
        builder.HasIndex(c => c.Concepto);
        builder.Property(c => c.Etiqueta).HasMaxLength(150).IsRequired();
    }
}
