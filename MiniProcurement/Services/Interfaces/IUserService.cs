using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Interfaces
{
    public interface IUserService
    {
        Task AssignRole(string roleName, int id);
        Task AssignDepartment(int id, int departmentId);
        Task<CreateUserResponseDto> CreateUser(CreateUserDto createUserDto);
        Task DeleteUser(int id);
        Task<IEnumerable<GetUserDto>> GetAllUsers();
        Task<GetUserDto> GetUserById(int id);
        Task UpdateUserName(string newUsername, int id);
    }
}