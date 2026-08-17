// Services/AuthService.cs
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Exceptions;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private const int DefaultUserRoleId = 2;   // matches your seeded "User" role

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (emailExists)
            throw new ConflictException("An account with this email already exists.");

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            RoleId = DefaultUserRoleId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Reload with Role included so the token service has RoleName available
        var savedUser = await _context.Users.Include(u => u.Role).FirstAsync(u => u.Id == user.Id);

        return BuildAuthResponse(savedUser);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedException("This account has been deactivated.");

        return BuildAuthResponse(user);
    }

    private AuthResponseDto BuildAuthResponse(User user) => new()
    {
        Token = _tokenService.GenerateToken(user),
        Email = user.Email,
        UserName = user.UserName,
        RoleName = user.Role.RoleName
    };
}