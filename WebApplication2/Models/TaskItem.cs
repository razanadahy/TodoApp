using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class TaskItem
{
    public int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public string Title { get; set; }=string.Empty;
    [MaxLength(2000)]
    public string? Description { get; set; } 
    public TaskStatus Status { get; set; } =  TaskStatus.Todo;
    public int TaskListId { get; set; }
    public TaskList TaskList { get; set; }
}