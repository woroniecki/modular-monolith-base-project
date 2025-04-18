using Modules.Core.API;
using Modules.UserManagement.API;
using SharedUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSharedFrameworkApi(builder.Host, builder.Configuration);

builder.Services.AddUserManagementModule();
builder.Services.AddCoreModule();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        var origin = builder.Configuration.GetSection("Cors")["Origin"];

        if (origin == "*")
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            policy.WithOrigins(origin)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

app.UseApiSharedFramework();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
