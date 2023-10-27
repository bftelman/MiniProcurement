using AutoMapper;
using MiniProcurement.Data.Contracts.Role;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, GetRoleDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users!.Select(u => u.FullName)));
    }
}