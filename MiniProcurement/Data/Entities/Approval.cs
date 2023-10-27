namespace MiniProcurement.Data.Entities;

public class Approval
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; }
    public int Order { get; set; }
    public bool HasApproved { get; set; }
}