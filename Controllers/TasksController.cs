using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Services;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService) => _taskService = taskService;

    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAll()
    {
        return Ok(await _taskService.GetAllTasksAsync());
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create(CreateTaskDto dto)
    {
        var created = await _taskService.CreateTaskAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [HttpPut("{id}/complete")]
    public async Task<ActionResult<TaskDto>> Complete(int id)
    {
        var result = await _taskService.CompleteTaskAsync(id);
        return Ok(result);
    }
}