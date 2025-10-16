using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Users
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = String.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; }=  String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<TaskList> TaskList { get; set; } = new();
}