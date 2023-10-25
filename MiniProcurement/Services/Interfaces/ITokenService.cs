using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}