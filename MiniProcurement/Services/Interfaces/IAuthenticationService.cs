using MiniProcurement.Data.Contracts.Authentication;
using MiniProcurement.Data.Contracts.User;

namespace MiniProcurement.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<CreateUserResponseDto> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
    }
}