using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Models.Identity;
using Template.Api.Controllers.Services.Contracts.Identity;
using Template.Api.Controllers.Services.Identity;

namespace Template.Api.Controllers.Registration;

public static class IdentityServicesRegistration
{
    public static void AddIdentityServices(this WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

        var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
        builder.Services.Configure<JwtSettings>(jwtSection);

        builder.Services.AddDbContext<AuthDbContext>(options =>
           options.UseSqlite(builder.Configuration.GetConnectionString("AccountsApiConnectionString")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<IUserService, UserService>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            };
        });
    }
}
