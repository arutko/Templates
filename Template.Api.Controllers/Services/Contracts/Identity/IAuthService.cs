using Template.Api.Controllers.Models.Identity;
using Template.Common;

namespace Template.Api.Controllers.Services.Contracts.Identity;

public interface IAuthService
{
    Task<Result<AuthResponse>> Login(AuthRequest request);
    Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
}
