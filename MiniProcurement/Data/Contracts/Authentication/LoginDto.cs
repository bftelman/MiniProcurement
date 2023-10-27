using System.ComponentModel.DataAnnotations;

namespace MiniProcurement.Data.Contracts.Authentication;

public class LoginDto
{
    [Required] public required string UserName { get; set; }

    [Required] public required string Password { get; set; }
}