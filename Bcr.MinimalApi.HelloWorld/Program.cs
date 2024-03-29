using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});
builder.Services.AddHealthChecks();
builder.Services.AddHostedService<MyBackgroundService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Log the application start
{
    var assembly = Assembly.GetExecutingAssembly();
    var appName = assembly.GetName().Name;
    var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";

    logger.LogInformation("Starting {AppName} {Version}", appName, version);
}

app.MapGet("/", () => "Hello World!");
app.MapHealthChecks("/healthz");

app.Run();

logger.LogInformation("Application shut down");
