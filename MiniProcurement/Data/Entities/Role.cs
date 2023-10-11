namespace MiniProcurement.Data.Entities;

public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<UserRole>? Roles { get; set; }
}

