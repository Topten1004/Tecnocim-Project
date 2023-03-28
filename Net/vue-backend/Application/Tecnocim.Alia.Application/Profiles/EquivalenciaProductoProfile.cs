using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaProductoProfile : Profile
{
    public EquivalenciaProductoProfile()
    {
        CreateMap<EquivalenciasProducto, EquivalenciaProductoDto>();
    }
}
