using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Models.Identity;
using Template.Api.Controllers.Services.Contracts.Identity;

namespace Template.Api.Controllers.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        var result = await _authenticationService.Login(request);

        if (result.Error == ApiErrors.WrongUserCredentials) return Unauthorized(result.Error);
        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var result = await _authenticationService.Register(request);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
