using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;
using TaskTrackerApi.Repositories;
using TaskTrackerApi.Exceptions;


namespace TaskTrackerApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository) => _repository = repository;

    // TaskService.cs
public async Task<PagedResultDto<TaskDto>> GetAllTasksAsync(int page, int pageSize)
{
    var (tasks, totalCount) = await _repository.GetAllAsync(page, pageSize);

    return new PagedResultDto<TaskDto>
    {
        Items = tasks.Select(MapToDto).ToList(),
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount
    };
}

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
    {
        var task = new TaskItem { Title = dto.Title };
        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();
        return MapToDto(task);
    }

    public async Task<TaskDto> CompleteTaskAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null) 
            throw new TaskNotFoundException(id);

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