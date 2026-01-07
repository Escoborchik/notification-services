using EmailNotificationService;
using Framework.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration, builder.Environment);
builder.Host.UseApplicationLogging(builder.Configuration);

var app = builder.Build();

await app.Configure();

app.Run();
