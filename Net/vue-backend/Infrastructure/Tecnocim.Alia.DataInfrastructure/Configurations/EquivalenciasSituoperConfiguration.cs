using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasSituoperConfiguration : IEntityTypeConfiguration<EquivalenciasSituoper>
{
    public void Configure(EntityTypeBuilder<EquivalenciasSituoper> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasSituoper");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(30).IsRequired();
        builder.Property(c => c.Descripcion).HasMaxLength(100).IsRequired(false);
    }
}
