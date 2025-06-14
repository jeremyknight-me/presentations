using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;
using SampleWeb.Persistence;
using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.DatabasePer.V1.Standalone;

public class LookupEndpointsTests : IAsyncLifetime
{
    private readonly MsSqlContainer container = new MsSqlBuilder().Build();

    public async ValueTask InitializeAsync()
        => await this.container.StartAsync();

    public async ValueTask DisposeAsync()
        => await this.container.DisposeAsync();

    public sealed class Api : WebApplicationFactory<Program>, IClassFixture<LookupEndpointsTests>
    {
        private readonly string connectionString;

        public Api(LookupEndpointsTests endpointsTests)
        {
            this.connectionString = endpointsTests.container.GetConnectionString();
        }

        public CancellationToken CT => TestContext.Current.CancellationToken;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.UseSetting("URLS", "https://+");
            builder.UseSetting("ConnectionStrings:Simple", this.connectionString);
        }

        [Fact]
        public async Task GetAll()
        {
            using var httpClient = this.CreateClient();
            var response
                = await httpClient.GetFromJsonAsync<IEnumerable<LookupResponse>>("/lookups", this.CT);
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetById_Invalid()
        {
            using var httpClient = this.CreateClient();
            var response = await httpClient.GetAsync($"/lookups/{1127}", this.CT);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetById_Valid()
        {
            using var httpClient = this.CreateClient();
            var response = await httpClient.GetFromJsonAsync<LookupResponse>($"/lookups/{50}", this.CT);
            Assert.NotNull(response);
            Assert.Equal(50, response.Id);
        }

        [Fact]
        public async Task Post()
        {
            var request = new LookupCreateRequest { Name = "Hello World!" };
            using var httpClient = this.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/lookups", request, this.CT);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync(cancellationToken: this.CT);
            var serializerOptions = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var lookupResponse = JsonSerializer.Deserialize<LookupResponse>(content, serializerOptions);
            using (var scope = this.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
                var entity = await context.Lookups.FindAsync(lookupResponse.Id);
                Assert.NotNull(entity);
                Assert.Equal("Hello World!", entity.Name);
            }
        }

        [Fact]
        public async Task Delete()
        {
            using var httpClient = this.CreateClient();
            var response = await httpClient.DeleteAsync($"/lookups/{40}", this.CT);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            using (var scope = this.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
                var entity = await context.Lookups.FindAsync(40);
                Assert.Null(entity);
            }
        }
    }
}
