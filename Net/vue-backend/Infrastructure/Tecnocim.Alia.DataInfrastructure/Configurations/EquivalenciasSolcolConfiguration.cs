using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class EquivalenciasSolcolConfiguration : IEntityTypeConfiguration<EquivalenciasSolcol>
{
    public void Configure(EntityTypeBuilder<EquivalenciasSolcol> builder)
    {
        builder.HasKey(c => c.Id).HasName("PK_EquivalenciasSolcol");
        builder.Property(c => c.Id).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.Tipo).HasMaxLength(20).IsRequired();
    }
}
