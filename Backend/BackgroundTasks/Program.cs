using BackgroundTasks.Tasks.Core;
using Coravel;
using Modules.Core.API;
using Modules.UserManagement.API;
using SharedUtils;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSharedFrameworkBgTasks(builder.Configuration);

builder.Services.AddUserManagementModule();
builder.Services.AddCoreModule();

builder.Services.AddScheduler();
builder.Services.AddTransient<TestTask>();

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<TestTask>().EveryFifteenMinutes();
});

app.Run();
