using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.DTO;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // Tous les endpoints nécessitent authentification
public class TaskListsController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskListsController> _logger;

    public TaskListsController(ITaskService taskService, ILogger<TaskListsController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    /// <summary>
    /// Récupérer toutes les listes de tâches de l'utilisateur connecté
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyTaskLists()
    {
        // Extraire l'ID de l'utilisateur depuis le token JWT
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        _logger.LogInformation("User {UserId} is fetching their task lists", userId);

        var lists = await _taskService.GetAllTaskListAsync(userId);
        return Ok(lists);
    }

    /// <summary>
    /// Créer une nouvelle liste de tâches pour l'utilisateur connecté
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] TaskRegisterDTO dto)
    {
        // Extraire l'ID de l'utilisateur depuis le token JWT
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // SÉCURITÉ : Forcer l'userId depuis le token (ignorer celui du body)
        dto.UserId = userId;

        _logger.LogInformation("User {UserId} is creating a new task list: {Title}", userId, dto.Title);

        var taskList = await _taskService.CreateTaskListAsync(dto);
        return CreatedAtAction(nameof(GetMyTaskLists), new { id = taskList.Id }, taskList);
    }

    /// <summary>
    /// Créer un nouvel item dans une liste de tâches
    /// </summary>
    [HttpPost("items")]
    public async Task<IActionResult> CreateTaskItem([FromBody] TaskItemDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        _logger.LogInformation("User {UserId} is creating a new task item in list {TaskListId}", userId, dto.TaskListId);

        var taskItem = await _taskService.CreateTaskItemAsync(dto);
        return Ok(taskItem);
    }
}
