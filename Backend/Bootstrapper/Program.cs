using Modules.Core.API;
using Modules.UserManagement.API;
using SharedUtils;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.AddSharedFramework(builder.Configuration);

builder.Services.AddUserManagementModule();
builder.Services.AddCoreModule();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseSharedFramework();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
