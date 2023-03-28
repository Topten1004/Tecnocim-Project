using System.Text.Json.Serialization;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string NombreRol { get; set; }
    public string JwtToken { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; }

    public AuthenticateResponse()
    {

    }

    public AuthenticateResponse(UsuarioDto usuario, string nombreRol, string jwtToken, string refreshToken)
    {
        Id = usuario.UsuarioId;
        Nombre = usuario.Nombre;
        Apellidos = usuario.Apellidos;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        NombreRol = usuario.NombreRol;
    }
}
