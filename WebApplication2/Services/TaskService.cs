using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Repositories;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class TaskService: ITaskService
{
    private readonly TaskListImpl taskListImpl;
    private readonly ILogger<AuthService> _logger;

    public TaskService(TaskListImpl taskListImpl, ILogger<AuthService> logger)
    {
        this.taskListImpl = taskListImpl;
        _logger = logger;
    }

    public async Task<List<TaskListDTO>> GetAllTaskListAsync(int userId)
    {
        _logger.LogInformation("Get all task list user : {userId}", userId);
        var taskList = await taskListImpl.GetByUserIdAsync(userId, 0, 10);

        return taskList.Select(tl => new TaskListDTO
        {
            Id = tl.Id,
            Title = tl.Title,
            Description = tl.Description,
            Users = tl.Users,
            ListTaskItem = tl.ListTaskItem.Select(it=>new TaskItemDTO
            {
                Title = it.Title,
                Description = it.Description,
                Status = it.Status,
                TaskListId = tl.Id
            }).ToList()
        }).ToList();
    }

    public async Task<TaskListDTO> CreateTaskListAsync(TaskRegisterDTO taskRegisterDTO)
    {
        _logger.LogInformation("Create task list user : {userId}", taskRegisterDTO.UserId);
        var task = new TaskList
        {
            Title = taskRegisterDTO.Title,
            Description = taskRegisterDTO.Description,
            UserId = taskRegisterDTO.UserId,
        };
        task = await taskListImpl.createTaskListAsync(task);
        return new TaskListDTO
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Users = task.Users,
            ListTaskItem = []
        };
    }

    public async Task<TaskItem> CreateTaskItemAsync(TaskItemDTO taskItemDTO)
    {
        _logger.LogInformation("Create task item");
        var taskItem = new TaskItem
        {
            Title = taskItemDTO.Title,
            Description = taskItemDTO.Description,
            Status = taskItemDTO.Status,
            TaskListId = taskItemDTO.TaskListId
        };
        taskItem = await taskListImpl.createTaskItemAsync(taskItem);
        return taskItem;
    }
}