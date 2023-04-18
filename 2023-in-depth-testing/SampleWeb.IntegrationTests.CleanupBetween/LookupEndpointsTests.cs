using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DataPersistence;
using Microsoft.Extensions.DependencyInjection;
using SampleWeb.Requests;
using SampleWeb.Responses;

namespace SampleWeb.IntegrationTests.CleanupBetween;

[Collection(SharedCollection.CollectionName)]
public class LookupEndpointsTests : IAsyncLifetime
{
    private readonly SampleWebApiFactory factory;
    private readonly HttpClient httpClient;

    public LookupEndpointsTests(SampleWebApiFactory webApplicationFactory)
    {
        this.factory = webApplicationFactory;
        this.httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll()
    {
        var response
                = await this.httpClient.GetFromJsonAsync<IEnumerable<LookupResponse>>("/lookups");
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetById_Invalid()
    {
        var response = await this.httpClient.GetAsync($"/lookups/{1127}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_Valid()
    {
        var response = await this.httpClient.GetFromJsonAsync<LookupResponse>($"/lookups/{50}");
        Assert.NotNull(response);
        Assert.Equal(50, response.Id);
    }

    [Fact]
    public async Task Post()
    {
        var request = new LookupPostRequest { Name = "Hello World!" };
        var response = await this.httpClient.PostAsJsonAsync("/lookups", request);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var serializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var lookupResponse = JsonSerializer.Deserialize<LookupResponse>(content, serializerOptions);
        using (var scope = this.factory.Services.CreateScope())
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
        var response = await this.httpClient.DeleteAsync($"/lookups/{40}");
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using (var scope = this.factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
            var entity = await context.Lookups.FindAsync(40);
            Assert.Null(entity);
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await this.factory.ResetDatabaseAsync();
}
