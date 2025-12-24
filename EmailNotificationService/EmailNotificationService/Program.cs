using EmailNotificationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration, builder.Environment);

var app = builder.Build();

app.Configure();

app.Run();
