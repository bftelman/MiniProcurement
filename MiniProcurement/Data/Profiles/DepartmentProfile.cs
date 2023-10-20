using AutoMapper;
using MiniProcurement.Data.Contracts.Department;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<Department, GetDepartmentDto>().ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users!.Select(u => u.FullName)));
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
}
