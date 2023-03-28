using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class PrevisionDemandasResponse
{
    public string Columnas { get; set; }
    public IEnumerable<TotalPrevisionDemandasStringDto> Listado { get; set; }
}
