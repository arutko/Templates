namespace Template.Api.Controllers.Models.Identity;

public class RegistrationResponse
{
    public string UserId { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
}
