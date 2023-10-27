using MiniProcurement.Data.Contracts.Role;

namespace MiniProcurement.Services.Interfaces;

public interface IRoleService
{
    Task CreateRole(string roleName);
    Task DeleteRole(int id);
    Task<IEnumerable<GetRoleDto>> GetAllRoles();
    Task<GetRoleDto> GetRoleById(int id);
    Task UpdateRole(int id, string roleName);
}