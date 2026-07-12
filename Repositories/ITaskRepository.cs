using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task SaveChangesAsync();
}