using Microsoft.AI.Foundry.Local;
using Microsoft.Extensions.Logging;

namespace FoundryLocal.Shared;

public static class FoundryLocalManagerFactory
{
    const string appName = "foundry_local_examples";

    public static async Task<FoundryLocalManager> MakeAsync<T>()
    {
        Configuration config = new()
        {
            AppName = appName,
            LogLevel = Microsoft.AI.Foundry.Local.LogLevel.Information
        };

        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
        });
        ILogger<T> logger = loggerFactory.CreateLogger<T>();

        await FoundryLocalManager.CreateAsync(config, logger, default);
        // Initialize the singleton instance
        return FoundryLocalManager.Instance;
    }
}
