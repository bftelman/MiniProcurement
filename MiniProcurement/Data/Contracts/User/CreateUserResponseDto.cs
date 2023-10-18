namespace MiniProcurement.Data.Contracts.User
{
    public record CreateUserResponseDto(int Id, string FullName, List<string> Roles, int DepartmentId);
}
