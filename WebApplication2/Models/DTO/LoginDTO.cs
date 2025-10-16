using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.DTO;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }=string.Empty;
    [Required]
    public string Password { get; set; }=string.Empty;
}