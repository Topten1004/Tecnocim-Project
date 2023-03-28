using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EmpresaConfiguracionesProfile : Profile
{
    public EmpresaConfiguracionesProfile()
    {
        CreateMap<EmpresaConfiguraciones, EmpresaConfiguracionesDto>()
            .ForMember(dto => dto.Fecha, x => x.MapFrom(d => d.Fecha.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}
