namespace WebApplication2.Models.DTO;

public class AuthResponseDTO
{
    public string Token { get; set; }=string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; }=string.Empty;
    public string Name { get; set; }=string.Empty;

    public UserRole Role { get; set; } = UserRole.User;

}