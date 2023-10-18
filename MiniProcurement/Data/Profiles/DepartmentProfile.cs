using AutoMapper;
using MiniProcurement.Data.Contracts.Department;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDepartmentDto>();
        }
    }
}
