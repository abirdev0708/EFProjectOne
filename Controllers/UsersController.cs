// Controllers/UsersController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Services;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserProfileService _profileService;

    public UsersController(IUserProfileService profileService) => _profileService = profileService;

    [HttpGet("me/profile")]
    public async Task<ActionResult<UserProfileDto>> GetMyProfile()
    {
        var userId = GetCurrentUserId();
        var profile = await _profileService.GetProfileAsync(userId);
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpPut("me/profile")]
    public async Task<ActionResult<UserProfileDto>> UpsertMyProfile(UpsertUserProfileDto dto)
    {
        var userId = GetCurrentUserId();
        var result = await _profileService.UpsertProfileAsync(userId, dto);
        return Ok(result);
    }

    private int GetCurrentUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? User.FindFirst("sub")?.Value;
        return int.Parse(idClaim!);
    }
}