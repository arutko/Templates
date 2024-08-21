using Serilog;
using Template.Api.Controllers.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistenceServices();
builder.AddIdentityServices();
builder.AddApplicationServices();
builder.AddInfrastructureServices();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
    builder.AddSwaggerGet();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();