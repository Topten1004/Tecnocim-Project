using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class CirbeConfiguration : IEntityTypeConfiguration<Cirbe>
{
    public void Configure(EntityTypeBuilder<Cirbe> builder)
    {
        builder.HasKey(c => c.CirbeId).HasName("PK_Cirbe");
        builder.Property(c => c.CirbeId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Operacion).HasMaxLength(100).IsRequired(false);
        builder.Property(c => c.Dispuesto).IsRequired();
        builder.Property(c => c.Participantes).IsRequired();
        builder.Property(c => c.Importes).IsRequired();
        builder.Property(c => c.Demora).IsRequired();
        builder.Property(c => c.Disponible).IsRequired();

        builder.Property(c => c.ContratoId).IsRequired(false);
        builder.Property(c => c.DocumentoId).IsRequired();
        builder.Property(c => c.EquivalenciasEntidadId).IsRequired();
        builder.Property(c => c.EquivalenciasMonedaId).IsRequired();
        builder.Property(c => c.EquivalenciasNatintervId).IsRequired();
        builder.Property(c => c.EquivalenciasPlazoId).IsRequired();
        builder.Property(c => c.EquivalenciasProductoId).IsRequired();
        builder.Property(c => c.EquivalenciasSituoperId).IsRequired(false);
        builder.Property(c => c.EquivalenciasSolcolId).IsRequired(false);
        builder.Property(c => c.EquivalenciasTipoId).IsRequired();

        builder.HasOne(d => d.Contrato)
                .WithMany(p => p.Cirbes)
                .HasForeignKey(d => d.ContratoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.Documento)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.DocumentoId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.EquivalenciasEntidad)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasEntidadId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasMoneda)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasMonedaId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasNatinterv)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasNatintervId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasPlazo)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasPlazoId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasProducto)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasProductoId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasSituoper)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasSituoperId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasSolcol)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasSolcolId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.EquivalenciasTipo)
             .WithMany(p => p.Cirbes)
             .HasForeignKey(d => d.EquivalenciasTipoId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
    }
}
