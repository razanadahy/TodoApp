using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.DTO;

public class RegisterDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string UserName { get; set; }=string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }=string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; }=string.Empty;
    [Required]
    public UserRole Role { get; set; }=UserRole.User;
}