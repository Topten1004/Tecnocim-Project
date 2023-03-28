namespace Tecnocim.Alia.Application.Responses;

public class UploadFicheroResponse
{
    public long FicheroId { get; set; }
    public string Nombre { get; set; }
    public string Origen { get; set; }
    public DateTime FechaContenido { get; set; }
    public int? ExtractorId { get; set; } = null;
    public string Estado { get; set; } = null;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public int EmpresaId { get; set; }

    public long DocumentoId { get; set; }

    public List<ErroresResponse> Errores { get; set; } = null!;
}
