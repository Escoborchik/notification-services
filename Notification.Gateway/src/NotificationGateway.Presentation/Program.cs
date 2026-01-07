using Framework.Logging;
using NotificationGateway.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);
Console.WriteLine(
    $"SEQ = {builder.Configuration.GetConnectionString("Seq") ?? "NULL"}");
builder.Host.UseApplicationLogging(builder.Configuration);

var app = builder.Build();
Console.WriteLine(
    $"SEQ AFTER BUILD = {app.Configuration.GetConnectionString("Seq") ?? "NULL"}");
await app.Configure();

app.Run();

namespace NotificationGateway.Presentation 
{
    public partial class Program;
}