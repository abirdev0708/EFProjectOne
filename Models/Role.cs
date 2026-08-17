// Models/Role.cs
namespace TaskTrackerApi.Models;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property — the "many" side: one Role has many Users
    public ICollection<User> Users { get; set; } = new List<User>();
}