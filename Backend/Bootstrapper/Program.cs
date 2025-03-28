using Modules.Core.API;
using Modules.UserManagement.API;
using SharedUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSharedFramework(builder.Configuration);

builder.Services.AddUserManagementModule();
builder.Services.AddCoreModule();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("Cors")["Origin"])
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

app.UseSharedFramework();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
