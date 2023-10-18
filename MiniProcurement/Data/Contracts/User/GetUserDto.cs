namespace MiniProcurement.Data.Contracts.User
{
    //public record GetUserDto(int Id, string FullName, List<string> Roles2, int DepartmentId);

    public class GetUserDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required IEnumerable<string> Roles { get; set; }
        public int DepartmentId { get; set; }
    }

}
