using DataPersistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.DatabasePer;

public class SampleWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer testContainer = new MsSqlBuilder().Build();

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

    public async Task InitializeAsync()
        => await this.testContainer.StartAsync();

    public new async Task DisposeAsync()
        => await this.testContainer.DisposeAsync();
}
