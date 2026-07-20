using System.ComponentModel.DataAnnotations;
namespace TaskTrackerApi.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTaskDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]

    public string Title { get; set; } = string.Empty;
}