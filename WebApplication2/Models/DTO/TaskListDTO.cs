namespace WebApplication2.Models.DTO;

public class TaskListDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public Users Users { get; set; } = null!;
    public List<TaskItemDTO> ListTaskItem { get; set; } = new List<TaskItemDTO>();
}