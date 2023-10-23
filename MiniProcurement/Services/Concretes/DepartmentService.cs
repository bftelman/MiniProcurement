using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Department;
using MiniProcurement.Data.Entities;
using MiniProcurement.Exceptions;
using MiniProcurement.Resources.Localization;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExceptionLoc> _localizer;


        public DepartmentService(ApplicationDbContext context, IMapper mapper, IStringLocalizer<ExceptionLoc> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<GetDepartmentDto>> GetAllDepartments()
        {
            var unmappedDeps = await _context.Departments.Include(d => d.Users).ToListAsync();
            var mappedDeps = _mapper.Map<IEnumerable<GetDepartmentDto>>(unmappedDeps);
            return mappedDeps;
        }

        public async Task<GetDepartmentDto> GetDepartmentById(int id)
        {
            var unmappedDep = await _context.Departments.FindAsync(id) ?? throw new NotFoundException(_localizer["DepartmentNotFound"]);
            var mappedDep = _mapper.Map<GetDepartmentDto>(unmappedDep);
            return mappedDep;
        }

        public async Task CreateDepartment(CreateDepartmentDto createUserDepartmentDto)
        {
            var department = _mapper.Map<Department>(createUserDepartmentDto);
            var user = await _context.Users.Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == department.ManagerUserId) ?? throw new NotFoundException(_localizer["UserNotFound"]);

            if (user.Roles != null && user.Roles.Any(r => r.Name == "MANAGER"))
            {
                department.Users = new List<User> { user };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotAuthorizedException(_localizer["NoManagerRights"]);
            }


        }

        public async Task UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await _context.Departments.FindAsync(id) ?? throw new NotFoundException(_localizer["DepartmentNotFound"]);
            _mapper.Map(updateDepartmentDto, department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
