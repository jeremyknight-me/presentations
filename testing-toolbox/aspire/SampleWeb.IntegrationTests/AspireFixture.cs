using System.Data.Common;
using Aspire.AppHost;
using Aspire.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using SampleWeb.Persistence;

namespace SampleWeb.IntegrationTests;

public class AspireFixture : IAsyncLifetime
{
    private DistributedApplication app = default!;
    private DbConnection connection = default!;
    private Respawner respawner = default!;
    private string connectionString = default!;

    public async ValueTask InitializeAsync()
    {
        await this.InitializeDistributedApplication();
        await this.InitializeRespawner();
    }

    public async ValueTask DisposeAsync()
    {
        await this.connection.DisposeAsync();
        await this.app.DisposeAsync();
    }

    public SimpleContext MakeContext()
    {
        var options = new DbContextOptionsBuilder<SimpleContext>()
            .UseSqlServer(this.connectionString)
            .Options;
        return new SimpleContext(options);
    }

    public async ValueTask<HttpClient> MakeHttpClient()
        => this.app.CreateHttpClient(ResourceNames.WebApi);

    public async Task ResetDatabaseAsync()
        => await this.respawner.ResetAsync(this.connection);

    private async ValueTask InitializeDistributedApplication()
    {
        var host = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Aspire_AppHost>();
        host.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        this.RemoveContainerPersistanceAnnotations(host.Resources);

        // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging

        this.app = await host.BuildAsync();
        await this.app.StartAsync();

        var resourceNotificationService = this.app.Services.GetRequiredService<ResourceNotificationService>();
        await resourceNotificationService
            .WaitForResourceAsync(ResourceNames.MigrationService, KnownResourceStates.Finished)
            .WaitAsync(TimeSpan.FromSeconds(30));
        await resourceNotificationService
            .WaitForResourceAsync(ResourceNames.WebApi, KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));

        this.connectionString = await this.app.GetConnectionStringAsync(ResourceNames.Database)
            ?? throw new InvalidOperationException("");
    }

    private async ValueTask InitializeRespawner()
    {
        this.connection = new SqlConnection(this.connectionString);
        await this.connection.OpenAsync();
        this.respawner = await Respawner.CreateAsync(this.connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            //SchemasToExclude
            SchemasToInclude = ["dbo"]
            //TablesToIgnore
            //TablesToInclude
        });
    }

    private void RemoveContainerPersistanceAnnotations(IResourceCollection resources)
    {
        foreach (var resource in resources)
        {
            var lifetimeAnnotaitons = resource.Annotations.OfType<ContainerLifetimeAnnotation>().ToList();
            foreach (var lifetimeAnnotation in lifetimeAnnotaitons)
            {
                resource.Annotations.Remove(lifetimeAnnotation);
            }
        }
    }
}
