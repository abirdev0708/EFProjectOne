using TaskTrackerApi.DTOs;

namespace TaskTrackerApi.Services;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasksAsync();
    Task<TaskDto> CreateTaskAsync(CreateTaskDto dto);
    Task<TaskDto> CompleteTaskAsync(int id);
}