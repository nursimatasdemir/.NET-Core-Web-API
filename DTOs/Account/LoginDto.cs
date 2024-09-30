using Microsoft.Build.Framework;

namespace api.DTOs.Account;

public class LoginDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
    
}