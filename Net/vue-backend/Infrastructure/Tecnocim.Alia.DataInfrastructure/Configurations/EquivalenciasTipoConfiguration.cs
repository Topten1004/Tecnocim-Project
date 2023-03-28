using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasTipoConfiguration : IEntityTypeConfiguration<EquivalenciasTipo>
{
    public void Configure(EntityTypeBuilder<EquivalenciasTipo> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasTipo");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(30).IsRequired();
        builder.Property(c => c.Descripcion).HasMaxLength(50).IsRequired(false);
    }
}