using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.HasKey(c => c.DocumentoId).HasName("PK_Documento");
        builder.Property(c => c.DocumentoId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.RutaDocumento).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Fecha).IsRequired();
        builder.Property(c => c.Origen).HasMaxLength(9).IsRequired();
        builder.Property(c => c.Status).IsRequired();
        builder.Property(c => c.ExtractorId).IsRequired(false);
        builder.Property(c => c.EmpresaId).IsRequired(true);

        builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Documentos)
                .HasForeignKey(d => d.EmpresaId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
