using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found. Please provide a valid id");
            return role;
        }

        public async Task CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            _context.Roles.Update(role);
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
