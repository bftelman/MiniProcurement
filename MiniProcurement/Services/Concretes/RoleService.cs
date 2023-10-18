using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Role;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRoleDto>> GetAllRoles()
        {
            var unmappedRoles = await _context.Roles.Include(r => r.Users).ToListAsync();
            var roles = _mapper.Map<IEnumerable<GetRoleDto>>(unmappedRoles);
            return roles;
        }

        public async Task<GetRoleDto> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found. Please provide a valid id");
            var mappedRole = _mapper.Map<GetRoleDto>(role);
            return mappedRole;
        }

        public async Task CreateRole(string roleName)
        {
            var role = new Role { Name = roleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(int id, string roleName)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found. Please provide a valid id");
            role.Name = roleName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found. Please provide a valid id");
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
