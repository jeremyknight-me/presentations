using DataPersistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.DatabasePer;

public class SampleWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureTestServices(services =>
        {
            var typesToRemove = new[]
            {
                typeof(DbContextOptions<SimpleContext>),
                typeof(SimpleContext),
            };

            foreach (var typeToRemove in typesToRemove)
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeToRemove);
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
            }

            services.AddDbContext<SimpleContext>(options =>
            {
                options
                    .UseSqlServer(this.dbContainer.GetConnectionString());
            });

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SimpleContext>();
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        });

    public async Task InitializeAsync()
    {
        await this.dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await this.dbContainer.DisposeAsync();
    }
}
