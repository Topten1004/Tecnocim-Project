using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Dtos;

public class DocumentoErroresDto : DocumentoDto
{
    public IEnumerable<ErroresResponse> Errores { get; set; } = null!;
}
