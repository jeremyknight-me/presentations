using System.Net.Http.Json;
using SampleWeb.Endpoints.Lookups;

namespace SampleWeb.IntegrationTests.WebApi;

public class LookupEndpointsTests : IClassFixture<SampleWebApiFactory>
{
    private readonly SampleWebApiFactory factory;
    private readonly HttpClient httpClient;

    public LookupEndpointsTests(SampleWebApiFactory webApplicationFactory)
    {
        this.factory = webApplicationFactory;
        this.httpClient = this.factory.CreateClient();
    }

    [Fact]
    public async Task Get()
    {
        var response
                = await this.httpClient.GetFromJsonAsync<IEnumerable<LookupResponse>>("/lookups");
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }
}
