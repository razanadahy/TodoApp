namespace WebApplication2.Models.DTO;

public class TaskItemDTO
{
    public string Title { get; set; }=string.Empty;
    public string? Description { get; set; } 
    public TaskStatus Status { get; set; } =  TaskStatus.Todo;
    public int TaskListId { get; set; }
}