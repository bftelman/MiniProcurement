namespace MiniProcurement.Data.Entities;
public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required string FullName { get; set; }
    public List<Role>? Roles { get; set; }
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public List<Document>? Documents { get; set; }

}

