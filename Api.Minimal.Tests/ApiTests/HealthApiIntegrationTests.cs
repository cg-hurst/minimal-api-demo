using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Minimal.Tests.ApiTests
{
    public class HealthApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public HealthApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetHealth_ReturnsHealthy()
        {
            var response = await _client.GetAsync("/health");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Deserialize the response content
            var healthResponse = await response.Content.ReadFromJsonAsync<HealthResponse>();
            Assert.NotNull(healthResponse);
            Assert.Equal("Healthy", healthResponse.Health);
        }

        [Fact]
        public async Task GetCrash_ReturnsInternalServerError()
        {
            var response = await _client.GetAsync("/crash");
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task GetCrash_ReturnsDetail_InDevelopmentEnvironment()
        {
            var response = await _client.GetAsync("/crash");
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Simulated exception for testing purposes", content);
        }

        [Fact]
        public async Task GetCrash_DoesNotReturnDetail_InProductionEnvironment()
        {
            // Simulate production environment by creating a new client with production settings
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("Environment", "Test");
                });
            var client = factory.CreateClient();
            var response = await client.GetAsync("/crash");
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("Simulated exception for testing purposes", content);
        }


        // Health Response model
        public class HealthResponse
        {
            public string? Health { get; set; }
        }
    }
}
