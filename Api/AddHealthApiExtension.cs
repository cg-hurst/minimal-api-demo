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



            return app;
        }
    }
}
