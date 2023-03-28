using System.Text.Json.Serialization;

namespace Tecnocim.Alia.Domain;

public class RefreshToken
{
    [JsonIgnore]
    public int RefreshTokenId { get; set; }
    public string Token { get; set; } = null;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string CreatedByIp { get; set; } = null;
    public DateTime? Revoked { get; set; }
    public string RevokedByIp { get; set; } = null;
    public string ReplacedByToken { get; set; } = null;
    public string ReasonRevoked { get; set; } = null;
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = null;
}
