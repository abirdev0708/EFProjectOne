// Services/UserProfileService.cs
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;
using TaskTrackerApi.Repositories;

namespace TaskTrackerApi.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _repository;

    public UserProfileService(IUserProfileRepository repository) => _repository = repository;

    public async Task<UserProfileDto?> GetProfileAsync(int userId)
    {
        var profile = await _repository.GetByUserIdAsync(userId);
        return profile is null ? null : MapToDto(profile);
    }

    public async Task<UserProfileDto> UpsertProfileAsync(int userId, UpsertUserProfileDto dto)
    {
        var existing = await _repository.GetByUserIdAsync(userId);

        if (existing is null)
        {
            var newProfile = new UserProfile
            {
                UserId = userId,
                Bio = dto.Bio,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                ProfilePictureUrl = dto.ProfilePictureUrl,
                DateOfBirth = dto.DateOfBirth
            };
            await _repository.AddAsync(newProfile);
            await _repository.SaveChangesAsync();
            return MapToDto(newProfile);
        }

        existing.Bio = dto.Bio;
        existing.PhoneNumber = dto.PhoneNumber;
        existing.Address = dto.Address;
        existing.ProfilePictureUrl = dto.ProfilePictureUrl;
        existing.DateOfBirth = dto.DateOfBirth;
        await _repository.SaveChangesAsync();
        return MapToDto(existing);
    }

    private static UserProfileDto MapToDto(UserProfile p) => new()
    {
        UserId = p.UserId,
        Bio = p.Bio,
        PhoneNumber = p.PhoneNumber,
        Address = p.Address,
        ProfilePictureUrl = p.ProfilePictureUrl,
        DateOfBirth = p.DateOfBirth
    };
}