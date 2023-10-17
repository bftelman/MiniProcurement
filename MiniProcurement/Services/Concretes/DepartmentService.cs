using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Concretes
{
    public class DepartmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var user = await _context.Departments.FindAsync(id) ?? throw new Exception("Department not found. Please provide a valid id");
            return user;
        }

        public async Task CreateDepartment(CreateUserDepartmentDto createUserDepartmentDto)
        {
            var department = _mapper.Map<Department>(createUserDepartmentDto);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
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
