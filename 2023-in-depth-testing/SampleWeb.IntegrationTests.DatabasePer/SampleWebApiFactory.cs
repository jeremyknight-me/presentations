using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.DatabasePer;

public class SampleWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(context =>
        {
            // can manipulate program's IServicesCollection
        });
    }

    public async Task InitializeAsync()
    {
        await this.dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await this.dbContainer.DisposeAsync();
    }
}
