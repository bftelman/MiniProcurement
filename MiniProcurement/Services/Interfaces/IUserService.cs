using MiniProcurement.Data.Contracts.User;

namespace MiniProcurement.Services.Interfaces
{
    public interface IUserService
    {
        Task AssignRole(AssignRoleToUserDto assignRoleToUserDto, int id);
        Task AssignDepartment(int id, AssignDepartmentToUserDto assignDepartmentToUserDto);
        Task<CreateUserResponseDto> CreateUser(CreateUserDto createUserDto);
        Task DeleteUser(int id);
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
        Task<GetUserDto> GetUserById(int id);
        Task UpdateUserName(UpdateNameDto updateNameDto, int id);
    }
}