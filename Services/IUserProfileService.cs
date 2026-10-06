// Services/IUserProfileService.cs
using TaskTrackerApi.DTOs;

namespace TaskTrackerApi.Services;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetProfileAsync(int userId);
    Task<UserProfileDto> UpsertProfileAsync(int userId, UpsertUserProfileDto dto);
}