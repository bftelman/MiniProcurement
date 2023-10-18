namespace MiniProcurement.Data.Contracts.Department
{
    public class GetDepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ManagerId { get; set; }
        public required List<string> Users { get; set; }
    }
}
