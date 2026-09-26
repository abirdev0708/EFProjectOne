using TaskTrackerApi.DTOs;

namespace TaskTrackerApi.Services;

public interface ITaskService
{
// ITaskService.cs
    Task<PagedResultDto<TaskDto>> GetAllTasksAsync(int page, int pageSize);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto dto);
    Task<TaskDto> CompleteTaskAsync(int id);
}