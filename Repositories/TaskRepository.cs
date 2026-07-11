// Repositories/TaskRepository.cs
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<List<TaskItem>> GetAllAsync() =>
        await _context.Tasks.ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task AddAsync(TaskItem task) =>
        await _context.Tasks.AddAsync(task);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}