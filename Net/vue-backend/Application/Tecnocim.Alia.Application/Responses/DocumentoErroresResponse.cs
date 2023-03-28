using System.Text.Json.Serialization;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class DocumentoErroresResponse
{
    [JsonPropertyName("documentos")]
    public IEnumerable<DocumentoErroresDto> DocumentoErroresDtoList { get; set; }
}