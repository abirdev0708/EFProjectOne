using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<(List<TaskItem> Items, int TotalCount)> GetAllAsync(int page, int pageSize)
    {
        var query = _context.Tasks.OrderByDescending(t => t.Id);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task AddAsync(TaskItem task) =>
        await _context.Tasks.AddAsync(task);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}