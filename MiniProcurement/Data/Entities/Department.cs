namespace MiniProcurement.Data.Entities;
public class Department
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ManagerUserId { get; set; }
    public required User Manager { get; set; }
}
