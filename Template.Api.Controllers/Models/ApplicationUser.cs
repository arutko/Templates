using Microsoft.AspNetCore.Identity;

namespace Template.Api.Controllers.Models;

public class ApplicationUser : IdentityUser
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
