using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class PoolBancarioEstadosResponse
{
    public IEnumerable<PoolBancarioEstadosDto> PoolBancarioList { get; set; }
}
