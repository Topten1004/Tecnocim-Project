using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasPeriodificacionConfiguration : IEntityTypeConfiguration<EquivalenciasPeriodificacion>
{
    public void Configure(EntityTypeBuilder<EquivalenciasPeriodificacion> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasPeriodificacion");
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Nombre).HasMaxLength(20).IsRequired();
    }
}
