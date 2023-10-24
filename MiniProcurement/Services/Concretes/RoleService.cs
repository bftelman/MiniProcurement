using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Role;
using MiniProcurement.Data.Entities;
using MiniProcurement.Exceptions;
using MiniProcurement.Resources.Localization;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExceptionLoc> _localizer;

        public RoleService(ApplicationDbContext context, IMapper mapper, IStringLocalizer<ExceptionLoc> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<GetRoleDto>> GetAllRoles()
        {
            var unmappedRoles = await _context.Roles.Include(r => r.Users).ToListAsync();
            var roles = _mapper.Map<IEnumerable<GetRoleDto>>(unmappedRoles);
            return roles;
        }

        public async Task<GetRoleDto> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new NotFoundException(_localizer["RoleNotFound"]);
            var mappedRole = _mapper.Map<GetRoleDto>(role);
            return mappedRole;
        }

        public async Task CreateRole(string roleName)
        {
            var role = new Role { Name = roleName };

            if (await _context.Roles.AnyAsync(r => r.Name == roleName)) throw new ResourceExistsException("Role already exists. Please add another role");
            else _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(int id, string roleName)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new NotFoundException(_localizer["RoleNotFound"]);
            role.Name = roleName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id) ?? throw new NotFoundException(_localizer["RoleNotFound"]);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
