using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

namespace SimpleApi;

internal static class MigrationExtensions
{
    public static IHost ApplyTodoMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
        var connectionString = context.Database.GetConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return host;
        }

        var dataSourceSegment = "Data Source=";
        var index = connectionString.IndexOf(dataSourceSegment);
        var filePath = connectionString.Substring(index + dataSourceSegment.Length);

        var directory = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        context.Database.Migrate();
        return host;
    }
}