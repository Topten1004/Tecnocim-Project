using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.HasKey(c => c.RolId).HasName("PK_Rol");
        builder.Property(c => c.RolId).HasColumnName("RolId").UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Nombre).HasMaxLength(50).IsRequired();
    }
}
