using Template.Api.Controllers.Services;
using Template.Api.Controllers.Services.Contracts;

namespace Template.Api.Controllers.Registration;

public static class ApplicationServicesRegistration
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
    }
}
