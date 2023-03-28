using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class CuotaConfiguration : IEntityTypeConfiguration<Cuota>
{
    public void Configure(EntityTypeBuilder<Cuota> builder)
    {
        builder.HasKey(c => new { c.ContratoId, c.Fecha } ).HasName("PK_Cuota");
        builder.Property(c => c.ContratoId).ValueGeneratedNever().IsRequired();
        builder.Property(c => c.Fecha).ValueGeneratedNever().IsRequired();
        builder.Property(c => c.Carencia).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.Importe).IsRequired().HasDefaultValue(0);

        builder.HasOne(d => d.Contrato)
                .WithMany(p => p.Cuotas)
                .HasForeignKey(d => d.ContratoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
    }
}
