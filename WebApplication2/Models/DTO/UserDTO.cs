namespace WebApplication2.Models.DTO;

public class UserDTO
{
    public int UserId { get; set; }
    public string Email { get; set; }=string.Empty;
    public string Name { get; set; }=string.Empty;
    public DateTime created_at { get; set; }=DateTime.Now;
}