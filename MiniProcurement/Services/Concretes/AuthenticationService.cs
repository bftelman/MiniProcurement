using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Authentication;
using MiniProcurement.Data.Contracts.User;
using MiniProcurement.Data.Entities;
using MiniProcurement.Exceptions;
using MiniProcurement.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MiniProcurement.Services.Concretes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;


        public AuthenticationService(IMapper mapper, ApplicationDbContext context, ITokenService tokenService)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task Register(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
            {
                throw new ResourceExistsException("User with this username already exists");
            }



            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            user.Roles = new List<Role> { new Role { Name = "BASIC_USER" } };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<CreateUserResponseDto> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(U => U.Roles).SingleOrDefaultAsync(user => user.UserName == loginDto.UserName)
                ?? throw new NotFoundException("User not found. PLease provide a valid id");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    throw new Exception("Invalid password. Please check your password again.");
                }
            }

            var result = new CreateUserResponseDto
            {
                FullName = user.FullName,
                Id = user.Id,
                DepartmentId = user.DepartmentId,
                Token = _tokenService.CreateToken(user)
            };

            return result;
        }
    }
}
