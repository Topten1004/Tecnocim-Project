using Microsoft.AspNetCore.Http;

namespace Tecnocim.Alia.Application.Request;

public class UploadFicheroRequest
{
    public IFormFile Fichero { get; set; }
    public string Origen { get; set; }
    public int EmpresaId { get; set; }
    public DateTime FechaContenido { get; set; }
}
