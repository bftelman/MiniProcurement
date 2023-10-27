namespace MiniProcurement.Data.Contracts.Role;

public class GetRoleDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<string> Users { get; set; }
}