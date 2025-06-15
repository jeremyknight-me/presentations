using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SampleWeb.IntegrationTests.DatabasePer.V2.Helpers;

public abstract class ProgramWebApplicationFactory<T>
    : WebApplicationFactory<Program>, IClassFixture<T>
    where T : TestMsSqlContainerBase
{
    private readonly string connectionString;

    protected ProgramWebApplicationFactory(LookupEndpointsTests endpointsTests)
    {
        this.connectionString = endpointsTests.Container.GetConnectionString();
    }

    public CancellationToken CT => TestContext.Current.CancellationToken;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.UseSetting("URLS", "https://+");
        builder.UseSetting("ConnectionStrings:Simple", this.connectionString);
    }
}
