using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class DocumentoResponse
{
    public IEnumerable<DocumentoDto> DocumentoDtoList { get; set; }
}
