using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.User;
using MiniProcurement.Data.Entities;
using MiniProcurement.Exceptions;
using MiniProcurement.Resources.Localization;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IStringLocalizer<ExceptionLoc> _localizer;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper, IStringLocalizer<ExceptionLoc> localizer)
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<CreateUserResponseDto> CreateUser(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        user.Roles = new List<Role>();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<CreateUserResponseDto>(user);
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new NotFoundException(_localizer["UserNotFound"]);
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
        var unmappedUser = await _context.Users.Include(u => u.Roles)
                               .SingleOrDefaultAsync(u => u.Id == id)
                           ?? throw new NotFoundException(_localizer["UserNotFound"]);

        var user = _mapper.Map<GetUserDto>(unmappedUser);
        return user;
    }

    public async Task<User> GetUserByIdRaw(int id)
    {
        var user = await _context.Users.Include(u => u.Roles)
                       .SingleOrDefaultAsync(u => u.Id == id)
                   ?? throw new NotFoundException(_localizer["UserNotFound"]);
        return user;
    }


    public async Task AssignRole(AssignRoleToUserDto assignRoleToUserDto, int id)
    {
        var user = await _context.Users.Include(u => u.Roles)
                       .FirstOrDefaultAsync(u => u.Id == id)
                   ?? throw new NotFoundException(_localizer["UserNotFound"]);

        if (user.Roles!.Any(r => r.Name.ToLower() == assignRoleToUserDto.Name.ToLower()))
            throw new ResourceExistsException(_localizer["UserHasRole"]);

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == assignRoleToUserDto.Name)
                   ?? throw new NotFoundException(_localizer["RoleNotFound"]);

        user.Roles.Add(role);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserName(UpdateNameDto updateNameDto, int id)
    {
        var user = await _context.Users.FindAsync(id)
                   ?? throw new NotFoundException(_localizer["UserNotFound"]);
        user.FullName = updateNameDto.FullName;
        await _context.SaveChangesAsync();
    }

    public async Task AssignDepartment(int userId, AssignDepartmentToUserDto assignDepartmentToUserDto)
    {
        var user = await _context.Users.FindAsync(userId)
                   ?? throw new NotFoundException(_localizer["UserNotFound"]);
        var department = await _context.Departments.FindAsync(assignDepartmentToUserDto.DepartmentId)
                         ?? throw new NotFoundException(_localizer["DepartmentNotFound"]);
        user.DepartmentId = department.Id;

        await _context.SaveChangesAsync();
    }
}