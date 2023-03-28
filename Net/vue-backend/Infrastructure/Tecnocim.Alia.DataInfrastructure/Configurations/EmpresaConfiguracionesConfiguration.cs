using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EmpresaConfiguracionesConfiguration : IEntityTypeConfiguration<EmpresaConfiguraciones>
{
    public void Configure(EntityTypeBuilder<EmpresaConfiguraciones> builder)
    {
        builder.HasKey(c => c.EmpresaConfiguracionesId).HasName("PK_EmpresaConfiguraciones");
        builder.Property(c => c.EmpresaConfiguracionesId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.FicheroConfiguracion).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Fecha).IsRequired();
        builder.Property(c => c.PorDefecto).HasDefaultValue(false).IsRequired();

        builder.HasOne(d => d.Empresa)
                  .WithMany(p => p.EmpresaConfiguraciones)
                  .HasForeignKey(d => d.EmpresaId)
                  .OnDelete(DeleteBehavior.Cascade);
    }
}
