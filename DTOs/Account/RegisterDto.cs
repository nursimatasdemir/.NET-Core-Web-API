using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.DTOs.Account;

public class RegisterDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required] 
    public string? Password { get; set; }
}

    // protected void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);
    //     List<IdentityRole> roles = new List<IdentityRole>
    //     {
    //         new IdentityRole
    //         {
    //             Name = "Admin",
    //             NormalizedName = "ADMIN"
    //         },
    //         new IdentityRole
    //         {
    //             Name = "User",
    //             NormalizedName = "USER"
    //         },
    //     };
    //     builder.Entity<IdentityRole>().HasData(roles);
    // }
