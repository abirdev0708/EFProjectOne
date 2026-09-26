using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public interface ITaskRepository
{
    Task<(List<TaskItem> Items, int TotalCount)> GetAllAsync(int page, int pageSize);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task SaveChangesAsync();
}