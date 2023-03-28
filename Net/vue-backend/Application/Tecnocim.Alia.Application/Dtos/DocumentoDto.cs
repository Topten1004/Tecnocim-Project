namespace Tecnocim.Alia.Application.Dtos;

public class DocumentoDto
{
    public long DocumentoId { get; set; }
    public string RutaDocumento { get; set; }
    public string Fecha { get; set; }
    public int? ExtractorId { get; set; }
    public string Origen { get; set; }
    public bool Status { get; set; }
    public string Created { get; set; }
}
