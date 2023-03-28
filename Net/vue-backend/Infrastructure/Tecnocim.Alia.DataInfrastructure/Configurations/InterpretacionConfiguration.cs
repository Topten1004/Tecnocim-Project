using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Enums;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class InterpretacionConfiguration : IEntityTypeConfiguration<Interpretacion>
{
    public void Configure(EntityTypeBuilder<Interpretacion> builder)
    {
        builder.HasKey(c => c.InterpretacionId).HasName("PK_Interpretacion");
        builder.Property(c => c.InterpretacionId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Concepto).HasMaxLength(100).IsRequired();
        builder.HasIndex(c => c.Concepto);
        builder.Property(c => c.Nombre).HasMaxLength(150).IsRequired();
        builder.Property(c => c.ColorPositivo).HasMaxLength(50).IsRequired(false);
        builder.Property(c => c.ColorNegativo).HasMaxLength(50).IsRequired(false);
        builder.Property(c => c.IconoPositivo).HasMaxLength(50).IsRequired(false);
        builder.Property(c => c.IconoNegativo).HasMaxLength(50).IsRequired(false);
        builder.Property(c => c.Tipo).HasColumnType("integer").IsRequired(true).HasDefaultValue(TipoInterpretacion.Ratio);
    }
}
