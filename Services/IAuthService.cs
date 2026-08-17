// Services/IAuthService.cs
using TaskTrackerApi.DTOs;

namespace TaskTrackerApi.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}