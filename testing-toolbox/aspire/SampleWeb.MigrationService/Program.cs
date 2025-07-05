using Microsoft.EntityFrameworkCore;
using SampleWeb.Persistence;

namespace SampleWeb.MigrationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        builder.Services.AddDbContext<SimpleContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("sample-db")
                    ?? throw new InvalidOperationException("Connection string 'sample-db' not found.");
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}
