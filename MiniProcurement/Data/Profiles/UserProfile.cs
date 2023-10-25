using AutoMapper;
using MiniProcurement.Data.Contracts.User;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles!.Select(r => r.Name)));
        }
    }
}
