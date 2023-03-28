using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Services;

public interface IUsuarioService
{
    Task<AuthenticateResponse> Authenticate(LoginRequest request, string ipAddress);
    Task<GenericResult<bool>> LogoutAsync(int userId);
    Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
    Task RevokeToken(string token, string ipAddress);
}
