using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class BalanceSituacionResponse
{
    public string Nombre { get; set; }
    public IEnumerable<TotalBalanceSituacionStringDto> List { get; set; }
    public string Columnas { get; set; }
}
