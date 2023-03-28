using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.DataInfrastructure.Comparers;
using Tecnocim.Alia.DataInfrastructure.Converters;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.HasKey(c => c.ContratoId).HasName("PK_Contrato");
        builder.Property(c => c.ContratoId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Inicio).HasConversion<DateOnlyConverter, DateOnlyComparer>().IsRequired();
        builder.Property(c => c.Vencimiento).HasConversion<DateOnlyConverter, DateOnlyComparer>().IsRequired();
        builder.Property(c => c.Carencia).IsRequired();
        builder.Property(c => c.Precio).IsRequired();
        builder.Property(c => c.Limite).IsRequired();
        builder.Property(c => c.PlazosAmortizacion).IsRequired(false);
        builder.Property(c => c.Valoracion).IsRequired(false);
        builder.Property(c => c.Notas).HasMaxLength(1200).IsRequired(false);
        builder.Property(c => c.Digitalizada).IsRequired(false);
        builder.Property(c => c.Minimis).IsRequired(false);

        builder.Property(c => c.EquivalenciasEntidadId).IsRequired();
        builder.Property(c => c.EquivalenciasMonedaId).IsRequired();
        builder.Property(c => c.EquivalenciasProductoId).IsRequired();
        builder.Property(c => c.EquivalenciasPeriodificacionId).IsRequired();

        builder.HasOne(d => d.EquivalenciasEntidad)
                .WithMany(p => p.Contratos)
                .HasForeignKey(d => d.EquivalenciasEntidadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasMoneda)
             .WithMany(p => p.Contratos)
             .HasForeignKey(d => d.EquivalenciasMonedaId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasProducto)
             .WithMany(p => p.Contratos)
             .HasForeignKey(d => d.EquivalenciasProductoId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasPeriodificacion)
            .WithMany(p => p.Contratos)
            .HasForeignKey(d => d.EquivalenciasPeriodificacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
