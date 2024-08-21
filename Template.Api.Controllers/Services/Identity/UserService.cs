using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Services.Contracts.Identity;

namespace Template.Api.Controllers.Services.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public string? UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }// ?? throw new UserUnauthorizedException(); }

    public async Task<ApplicationUser?> GetUser(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<List<ApplicationUser>> GetUsersInRole(string role)
        => (await _userManager.GetUsersInRoleAsync(role)).ToList();
}
