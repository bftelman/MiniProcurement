namespace MiniProcurement.Data.Contracts.User;

public class CreateUserResponseDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public int? DepartmentId { get; set; }
    public required string Token { get; set; }
}