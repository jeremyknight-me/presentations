using System.Text.Json;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;

namespace SampleWeb.IntegrationTests.Endpoints;

[Collection(AspireCollection.CollectionName)]
public  class LookupEndpointsTests
{
    private readonly AspireFixture fixture;

    public LookupEndpointsTests(AspireFixture fixture)
    {
        this.fixture = fixture;
    }

    public static CancellationToken CT => TestContext.Current.CancellationToken;

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;
    public async ValueTask DisposeAsync() => await this.fixture.ResetDatabaseAsync();

    [Fact]
    public async Task GetAll()
    {
        using var httpClient = await this.fixture.MakeHttpClient();
        var response = await httpClient
            .GetFromJsonAsync<IEnumerable<LookupResponse>>("/lookups", CT);
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetById_Invalid()
    {
        using var httpClient = await this.fixture.MakeHttpClient();
        var response = await httpClient
            .GetAsync($"/lookups/{1127}", CT);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_Valid()
    {
        using var httpClient = await this.fixture.MakeHttpClient();
        var response = await httpClient
            .GetFromJsonAsync<LookupResponse>($"/lookups/{50}", CT);
        Assert.NotNull(response);
        Assert.Equal(50, response.Id);
    }

    [Fact]
    public async Task Post()
    {
        var request = new LookupCreateRequest { Name = "Hello World!" };
        using var httpClient = await this.fixture.MakeHttpClient();
        var response = await httpClient
            .PostAsJsonAsync("/lookups", request, CT);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync(CT);
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

        using var context = this.fixture.MakeContext();
        var entity = await context.Lookups.FindAsync([lookupResponse.Id], CT);
        Assert.NotNull(entity);
        Assert.Equal("Hello World!", entity.Name);
    }

    [Fact]
    public async Task Delete()
    {
        using var httpClient = await this.fixture.MakeHttpClient();
        var response = await httpClient.DeleteAsync($"/lookups/{40}", CT);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var context = this.fixture.MakeContext();
        var entity = await context.Lookups.FindAsync([40], CT);
        Assert.Null(entity);
    }
}
