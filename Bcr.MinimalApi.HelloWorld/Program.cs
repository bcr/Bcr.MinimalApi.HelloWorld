using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
