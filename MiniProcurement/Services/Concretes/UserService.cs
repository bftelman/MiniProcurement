using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.User;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateUserResponseDto> CreateUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateUserResponseDto>(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found. Please provide a valid id");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
        {
            var unmappedUsers = await _context.Users.Include(u => u.Roles)
                                                    .ToListAsync();

            var users = _mapper.Map<IEnumerable<GetUserDto>>(unmappedUsers);

            return users;
        }

        public async Task<GetUserDto> GetUserById(int id)
        {
            var unmappedUser = await _context.Users.FindAsync(id) ?? throw new Exception("User not found. Go drink some water");
            var user = _mapper.Map<GetUserDto>(unmappedUser);
            return user;
        }

        public async Task AssignRole(AssignRoleToUserDto assignRoleToUserDto, int id)
        {
            var user = await _context.Users.Include(u => u.Roles)
                                           .FirstOrDefaultAsync(u => u.Id == id)

                                           ?? throw new Exception("User not found. Please provide a valid id");

            if (user.Roles!.Any(r => r.Name.ToLower() == assignRoleToUserDto.Name.ToLower()))
            {
                throw new Exception("User already has this role. Please provide a different role or remove this one");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == assignRoleToUserDto.Name);

            if (role is null)
            {
                throw new Exception("Role not found");
            }

            user.Roles!.Add(role);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserName(UpdateNameDto updateNameDto, int id)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new Exception("User not found. Please provide a valid id");
            user.FullName = updateNameDto.FullName;
            await _context.SaveChangesAsync();
        }

        public async Task AssignDepartment(int userId, AssignDepartmentToUserDto assignDepartmentToUserDto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found. Please provide a valid user id.");

            var department = await _context.Departments.FindAsync(assignDepartmentToUserDto.DepartmentId);

            if (department == null)
                throw new Exception("Department not found. Please provide a valid department id.");

            user.DepartmentId = department.Id;

            await _context.SaveChangesAsync();
        }
    }
}
