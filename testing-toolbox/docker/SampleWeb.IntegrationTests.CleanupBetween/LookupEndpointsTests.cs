using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;
using SampleWeb.Persistence;

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

    public CancellationToken CT => TestContext.Current.CancellationToken;

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;
    public async ValueTask DisposeAsync() => await this.factory.ResetDatabaseAsync();

    [Fact]
    public async Task GetAll()
    {
        var response = await this.httpClient
            .GetFromJsonAsync<IEnumerable<LookupResponse>>("/lookups", this.CT);
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetById_Invalid()
    {
        var response = await this.httpClient
            .GetAsync($"/lookups/{1127}", this.CT);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_Valid()
    {
        var response = await this.httpClient
            .GetFromJsonAsync<LookupResponse>($"/lookups/{50}", this.CT);
        Assert.NotNull(response);
        Assert.Equal(50, response.Id);
    }

    [Fact]
    public async Task Post()
    {
        var request = new LookupCreateRequest { Name = "Hello World!" };
        var response = await this.httpClient
            .PostAsJsonAsync("/lookups", request, this.CT);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync(this.CT);
        JsonSerializerOptions serializerOptions = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var lookupResponse = JsonSerializer.Deserialize<LookupResponse>(content, serializerOptions);
        if (lookupResponse is null)
        {
            return;
        }

        using var scope = this.factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
        var entity = await context.Lookups.FindAsync([lookupResponse.Id], this.CT);
        Assert.NotNull(entity);
        Assert.Equal("Hello World!", entity.Name);
    }

    [Fact]
    public async Task Delete()
    {
        var response = await this.httpClient.DeleteAsync($"/lookups/{40}", this.CT);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var scope = this.factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
        var entity = await context.Lookups.FindAsync([40], this.CT);
        Assert.Null(entity);
    }
}
