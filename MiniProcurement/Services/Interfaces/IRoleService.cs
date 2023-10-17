using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(Role role);
        Task DeleteRole(int id);
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task UpdateRole(Role role);
    }
}