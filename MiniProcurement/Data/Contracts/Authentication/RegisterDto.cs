using System.ComponentModel.DataAnnotations;

namespace MiniProcurement.Data.Contracts.Authentication;

public class RegisterDto
{
    [Required] public required string UserName { get; set; }

    [Required] public required string FullName { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 4)]
    public required string Password { get; set; }
}