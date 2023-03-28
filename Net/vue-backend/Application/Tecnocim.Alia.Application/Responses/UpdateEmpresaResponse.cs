using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class UpdateEmpresaResponse
{
    public UpdateEmpresaResponse()
    {
        Usuarios = new HashSet<UsuarioDto>();  
    }

    public int EmpresaId { get; set; }
    public string CIF { get; set; }
    public string Nombre { get; set; }
    public string Contacto { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }

    public ICollection<UsuarioDto> Usuarios { get; set; }
}
