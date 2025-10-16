namespace WebApplication2.Models.DTO;

public class TaskRegisterDTO
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int UserId { get; set; }
}