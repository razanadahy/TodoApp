using WebApplication2.Models;

namespace WebApplication2.Repositories.Interfaces;

public interface ItaskListRepository
{
    Task<List<TaskList>> GetByUserIdAsync(int userId, int skype, int take);
    Task<TaskList> createTaskListAsync(TaskList taskList);
    Task<TaskItem> createTaskItemAsync(TaskItem taskItem);
}