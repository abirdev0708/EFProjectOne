// DTOs/UserProfileDto.cs
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.DTOs;

public class UpsertUserProfileDto
{
    [MaxLength(500)]
    public string? Bio { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }
}

public class UserProfileDto
{
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
}