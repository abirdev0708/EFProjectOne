// Models/UserProfile.cs
namespace TaskTrackerApi.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // Foreign key — this is the "dependent" side of the 1:1 relationship
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}