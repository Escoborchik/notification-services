using NotificationGateway.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

await app.Configure();

app.Run();

namespace NotificationGateway.Presentation 
{
    public partial class Program;
}