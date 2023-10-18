using MiniProcurement.Data.Contracts.Department;

namespace MiniProcurement.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateDepartment(CreateDepartmentDto createUserDepartmentDto);
        Task DeleteDepartment(int id);
        Task<IEnumerable<GetDepartmentDto>> GetAllDepartments();
        Task<GetDepartmentDto> GetDepartmentById(int id);
        Task UpdateDepartment(int id, UpdateDepartmentDto department);
    }
}