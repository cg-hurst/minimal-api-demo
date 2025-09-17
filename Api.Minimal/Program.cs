using Api.Minimal.Api;
using Api.Minimal.Books.Services;
using Api.Minimal.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register the BookService as a singleton as it keeps state in memory
builder.Services
    .AddSingleton<BookService>()
    ;

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app
    .AddBookApi()
    .AddHealthApi()
    ;


app.Run();

// Make the Program class accessible for testing
public partial class Program { }
