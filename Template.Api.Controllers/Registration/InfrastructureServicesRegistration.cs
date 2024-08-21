using Mapster;
using MapsterMapper;
using Serilog;
using Template.Api.Controllers.Mapping;

namespace Template.Api.Controllers.Registration;

public static class InfrastructureServicesRegistration
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        var config = new TypeAdapterConfig();
        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();
        MapsterConfig.Configure();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
    }
}
