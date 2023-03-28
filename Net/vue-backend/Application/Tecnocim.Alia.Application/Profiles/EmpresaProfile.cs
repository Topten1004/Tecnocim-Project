using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<Empresa, EmpresaDto>();

        CreateMap<Empresa, EmpresaUsuarioDto>();

        CreateMap<CreateEmpresaRequest, Empresa>()
             .ForMember(e => e.Created, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.Updated, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.EmpresaConfiguraciones, e => e.Ignore())
            .ForMember(e => e.Usuarios, e => e.Ignore());

        CreateMap<Empresa, UpdateEmpresaResponse>();
    }
}
