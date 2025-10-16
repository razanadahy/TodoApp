using WebApplication2.Models;
using WebApplication2.Models.DTO;

namespace WebApplication2.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskListDTO>> GetAllTaskListAsync(int userId);
    Task<TaskListDTO> CreateTaskListAsync(TaskRegisterDTO taskRegisterDTO);
    Task<TaskItem> CreateTaskItemAsync(TaskItemDTO taskItemDTO);
}