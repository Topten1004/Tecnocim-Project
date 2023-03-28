using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class PoolConfiguration : IEntityTypeConfiguration<Pool>
{
    public void Configure(EntityTypeBuilder<Pool> builder)
    {
        builder.HasKey(c => c.PoolId).HasName("PK_Pool");
        builder.Property(c => c.PoolId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Cuenta).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Concepto).HasMaxLength(250).IsRequired();
        builder.Property(c => c.Dispuesto).IsRequired(false);

        builder.Property(c => c.ContratoId).IsRequired(false);
        builder.Property(c => c.DocumentoId).IsRequired();

        builder.HasOne(d => d.Contrato)
                .WithMany(p => p.Pools)
                .HasForeignKey(d => d.ContratoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.Documento)
             .WithMany(p => p.Pools)
             .HasForeignKey(d => d.DocumentoId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
