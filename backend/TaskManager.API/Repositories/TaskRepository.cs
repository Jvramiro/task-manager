using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext context;

    public TaskRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await context.Tasks.FindAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        await context.Tasks.AddAsync(task);
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem task)
    {
        var existingTask = await context.Tasks.FindAsync(id);
        if (existingTask == null) return null;

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Priority = task.Priority;
        existingTask.Status = task.Status;

        context.Tasks.Update(existingTask);
        return existingTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null) return false;

        context.Tasks.Remove(task);
        return true;
    }
}
