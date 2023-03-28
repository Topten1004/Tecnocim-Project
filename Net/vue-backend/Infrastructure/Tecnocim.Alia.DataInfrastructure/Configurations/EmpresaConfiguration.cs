using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(c => c.EmpresaId).HasName("PK_Empresa");
        builder.Property(c => c.EmpresaId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.CIF).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Nombre).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Contacto).HasMaxLength(50);
        builder.Property(c => c.Telefono).HasMaxLength(20);
        builder.Property(c => c.Email).HasMaxLength(50);
    }
}
