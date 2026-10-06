// Repositories/UserProfileRepository.cs
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _context;

    public UserProfileRepository(AppDbContext context) => _context = context;

    public async Task<UserProfile?> GetByUserIdAsync(int userId) =>
        await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

    public async Task AddAsync(UserProfile profile) =>
        await _context.UserProfiles.AddAsync(profile);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}