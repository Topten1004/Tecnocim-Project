using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ForMember(dto => dto.NombreRol, e => e.MapFrom(x => x.Rol.Nombre))
            .ReverseMap()
            .ForMember(e => e.Rol, e => e.Ignore());

        CreateMap<Usuario, UsuarioListadoDto>()
            .ForMember(dto => dto.NombreRol, e => e.MapFrom(x => x.Rol.Nombre))
            .ReverseMap();

        CreateMap<CreateUsuarioRequest, Usuario>()
            .ForMember(e => e.Created, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.Updated, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.Rol, e => e.Ignore());

        CreateMap<UpdateUsuarioRequest, Usuario>()
            .ForMember(e => e.Updated, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.Rol, e => e.Ignore());

        CreateMap<Usuario, UpdateUsuarioResponse>()
             .ForMember(dto => dto.NombreRol, e => e.MapFrom(x => x.Rol.Nombre));

    }
}
