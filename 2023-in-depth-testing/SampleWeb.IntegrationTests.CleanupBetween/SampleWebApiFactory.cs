using System.Data.Common;
using DataPersistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Respawn;
using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.CleanupBetween;

public class SampleWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer testContainer = new MsSqlBuilder().Build();
    private DbConnection connection = default!;
    private Respawner respawner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<SimpleContext>>();
            services.RemoveAll<SimpleContext>();

            services.AddDbContext<SimpleContext>(options =>
            {
                options
                    .UseSqlServer(this.testContainer.GetConnectionString());
            });

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        });

    public async Task ResetDatabaseAsync()
        => await this.respawner.ResetAsync(this.connection);

    public async Task InitializeAsync()
    {
        await this.testContainer.StartAsync();
        this.connection = new SqlConnection(this.testContainer.GetConnectionString());
        await this.InitializeRespawner();
    }

    private async Task InitializeRespawner()
    {
        await this.connection.OpenAsync();
        this.respawner = await Respawner.CreateAsync(this.connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            //SchemasToExclude
            SchemasToInclude = new[] { "dbo" }
            //TablesToIgnore
            //TablesToInclude
        });
    }

    public new async Task DisposeAsync() => await this.testContainer.DisposeAsync();
}
