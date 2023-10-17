using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contracts
{
    public record GetUserDto(int Id, string FullName, List<Role> Roles, int DepartmentId);
}
