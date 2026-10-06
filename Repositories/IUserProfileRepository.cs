// Repositories/IUserProfileRepository.cs
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByUserIdAsync(int userId);
    Task AddAsync(UserProfile profile);
    Task SaveChangesAsync();
}