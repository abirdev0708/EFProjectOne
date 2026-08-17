// Services/ITokenService.cs
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}