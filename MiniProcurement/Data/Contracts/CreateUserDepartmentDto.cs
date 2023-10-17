using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contracts
{
    public record CreateUserDepartmentDto(string Name, int ManagerUserId);
}
