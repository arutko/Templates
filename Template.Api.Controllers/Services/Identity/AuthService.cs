using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Models.Identity;
using Template.Api.Controllers.Services.Contracts;
using Template.Api.Controllers.Services.Contracts.Identity;
using Template.Common;

namespace Template.Api.Controllers.Services.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAccountService _accountService;
    private readonly ILogger<AuthService> _logger;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IAccountService accountService,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _accountService = accountService;
        _logger = logger;
    }

    public async Task<Result<AuthResponse>> Login(AuthRequest request)
    {
        _logger.LogInformation("Execution of Login");

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Wrong username");
            return Result<AuthResponse>.Failure(ApiErrors.WrongUserCredentials);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded == false)
        {
            _logger.LogWarning("Wrong password");
            return Result<AuthResponse>.Failure(ApiErrors.WrongUserCredentials);
        }

        var activeAccountExists = await _accountService.DoesAccountExists(user.AccountId);
        if (activeAccountExists == false)
        {
            _logger.LogWarning("Account doesnt exist");
            return Result<AuthResponse>.Failure(ApiErrors.NoActiveAccount);
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            AccountId = user.AccountId,
        };

        _logger.LogInformation("Logged in");
        return Result<AuthResponse>.Success(response);
    }


    public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var accountId = await _accountService.CreateAccount(
            Account.Create(true, request.EnrolledAt));

        if (accountId == null)
            return Result<RegistrationResponse>.Failure(ApiErrors.NoActiveAccount);

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true,
            AccountId = accountId.Value
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Person");
            return Result<RegistrationResponse>.Success(new RegistrationResponse() { UserId = user.Id, AccountId = accountId.Value });
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result.Errors)
                str.AppendFormat("•{0}\n", err.Description);

            return Result<RegistrationResponse>.Failure($"{str}");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
           signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}
