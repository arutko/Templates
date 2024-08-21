using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.Services.Contracts.Identity;

public interface IUserService
{
    Task<List<ApplicationUser>> GetUsersInRole(string role);
    Task<ApplicationUser?> GetUser(string userId);
    public string UserId { get; }
}
