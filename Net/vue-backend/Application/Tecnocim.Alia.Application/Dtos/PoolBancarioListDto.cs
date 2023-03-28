namespace Tecnocim.Alia.Application.Dtos;

public class PoolBancarioListDto
{
    public PoolBancarioListDto()
    {
        PoolBancarioList = new HashSet<PoolBancarioDto>();  
    }

    public ICollection<PoolBancarioDto> PoolBancarioList { get; set; }
}
