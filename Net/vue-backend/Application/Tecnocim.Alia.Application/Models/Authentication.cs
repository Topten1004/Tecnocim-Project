namespace Tecnocim.Alia.Application.Models;

public class Authentication
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessExpiration { get; set; }
    public int RefreshTokenTTL { get; set; }
}
