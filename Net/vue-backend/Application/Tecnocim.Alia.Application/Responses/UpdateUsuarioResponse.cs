using System.Text.Json.Serialization;

namespace Tecnocim.Alia.Application.Responses;

public class UpdateUsuarioResponse
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public string PuestoTrabajo { get; set; }
    public int RolId { get; set; }
    public string NombreRol { get; set; }
}
