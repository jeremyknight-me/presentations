namespace SampleWeb.IntegrationTests;

[Collection(AspireCollection.CollectionName)]
public class HealthChecksTests
{
    private readonly AspireFixture fixture;

    public HealthChecksTests(AspireFixture fixture)
    {
        this.fixture = fixture;
    }

    public static CancellationToken CT => TestContext.Current.CancellationToken;

    [Fact]
    public async Task HealthCheckEndpointReturnsOkStatusCode()
    {
        // Arrange
        using var client = await this.fixture.MakeHttpClient();

        // Act
        var response = await client.GetAsync("/health", CT);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
