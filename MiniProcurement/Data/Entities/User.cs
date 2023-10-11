namespace MiniProcurement.Data.Entities;
public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public List<UserRole>? Roles { get; set; }
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

}

