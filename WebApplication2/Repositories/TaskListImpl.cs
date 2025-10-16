using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Repositories;

public class TaskListImpl: ItaskListRepository
{
    private readonly AppDBContext _context;

    public TaskListImpl(AppDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<TaskList>> GetByUserIdAsync(int userId, int  skype, int take)
    {
        return await _context.TaskList.Include(tl => tl.ListTaskItem)
            // .Include(tl => tl.Users)
            .Where(tl => tl.UserId == userId)
            .Skip(skype)
            .Take(take)
            .ToListAsync();
    }

    public async Task<TaskList> createTaskListAsync(TaskList taskList)
    {
        _context.TaskList.Add(taskList);
        await _context.SaveChangesAsync();
        return taskList;
    }

    public async Task<TaskItem> createTaskItemAsync(TaskItem taskItem)
    {
        _context.TaskItem.Add(taskItem);
        await _context.SaveChangesAsync();
        return taskItem;
    }
}