using Api.Minimal.Books.Models;
using Api.Minimal.Books.Services;

namespace Api.Minimal.Api
{
    public static class AddHealthApiExtension
    {
        public static WebApplication AddHealthApi(this WebApplication app)
        {
            app
                .MapGet("/health", (BookService service) =>
                {
                    return Results.Ok(new { Health = "Healthy" });
                })
                .WithName("HealthCheck");


            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Test")
            {
                app
                    .MapGet("/crash", async (BookService service) =>
                    {
                        await Task.CompletedTask;
                        throw new Exception("Simulated exception for testing purposes.");
                    })
                    .WithName("SimulateException");
            }

            return app;
        }
    }
}
