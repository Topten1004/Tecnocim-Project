using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class RolProfile : Profile
{
    public RolProfile()
    {
        CreateMap<Rol, RolDto>();
    }
}
