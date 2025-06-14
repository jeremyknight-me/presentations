using System.Net.Http.Json;
using SampleWeb.Endpoints.Guids;

namespace SampleWeb.IntegrationTests.WebApi;

public class GuidEndpointsTests : IClassFixture<SampleWebApiFactory>
{
    private readonly SampleWebApiFactory factory;
    private readonly HttpClient httpClient;

    public GuidEndpointsTests(SampleWebApiFactory webApplicationFactory)
    {
        this.factory = webApplicationFactory;
        this.httpClient = this.factory.CreateClient();
    }

    [Fact]
    public async Task Get()
    {
        var response = await this.httpClient.GetFromJsonAsync<GuidResponse>("/guids");
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Guid);
    }
}
