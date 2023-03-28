using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Authentication;

public interface IJwtUtils
{
    public string GenerateJwtToken(UsuarioDto user);
    public int? ValidateJwtToken(string token);
    public RefreshTokenDto GenerateRefreshToken(string ipAddress);
}
