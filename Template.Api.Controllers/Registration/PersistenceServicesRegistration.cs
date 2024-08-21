using Microsoft.EntityFrameworkCore;
using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.DataStore.DataStore.Sqlite;

namespace Template.Api.Controllers.Registration;

public static class PersistenceServicesRegistration
{
    public static void AddPersistenceServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext")));

        builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("AuthDbContext")));

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}