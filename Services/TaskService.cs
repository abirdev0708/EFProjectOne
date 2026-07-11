// Services/TaskService.cs
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;
using TaskTrackerApi.Repositories;

namespace TaskTrackerApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository) => _repository = repository;

    public async Task<List<TaskDto>> GetAllTasksAsync()
    {
        var tasks = await _repository.GetAllAsync();
        return tasks.Select(MapToDto).ToList();
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
    {
        var task = new TaskItem { Title = dto.Title };
        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();
        return MapToDto(task);
    }

    public async Task<TaskDto?> CompleteTaskAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null) return null;

        task.IsComplete = true;
        await _repository.SaveChangesAsync();
        return MapToDto(task);
    }

    private static TaskDto MapToDto(TaskItem task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        IsComplete = task.IsComplete,
        CreatedAt = task.CreatedAt
    };
}