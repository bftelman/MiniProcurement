using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contracts
{
    public record CreateUserResponseDto(int Id, string FirstName, List<Role> roles, Department Department);
}
