using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SampleWeb.Persistence;

namespace SampleWeb.MigrationService;

public class Worker : BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private static readonly ActivitySource activitySource = new(ActivitySourceName);

    private readonly ILogger<Worker> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IHostApplicationLifetime hostApplicationLifetime;

    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = this.serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<SimpleContext>();

            await this.RunMigrationAsync(dbContext, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        this.hostApplicationLifetime.StopApplication();
    }

    private async Task RunMigrationAsync(SimpleContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        });
    }
}
