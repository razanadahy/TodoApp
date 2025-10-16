using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class TaskList
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = String.Empty;
    [Required]
    public string Description { get; set; } = String.Empty;
    public int UserId { get; set; }
    public Users Users { get; set; } = null!;
    public List<TaskItem> ListTaskItem = new List<TaskItem>();
}