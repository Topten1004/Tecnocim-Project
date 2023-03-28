using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(c => c.RefreshTokenId).HasName("PK_RefreshToken");
        builder.Property(c => c.RefreshTokenId).UseIdentityColumn(1).ValueGeneratedOnAdd();
        builder.Property(c => c.ReasonRevoked).IsRequired(false);
        builder.Property(c => c.RevokedByIp).IsRequired(false);
        builder.Property(c => c.ReplacedByToken).IsRequired(false);
        builder.HasIndex(e => e.UsuarioId, "IX_RefreshTokens_UsuarioId");

        builder.HasOne(d => d.Usuario)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UsuarioId);
    }
}
