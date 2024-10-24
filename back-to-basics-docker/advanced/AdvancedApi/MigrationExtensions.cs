using Microsoft.EntityFrameworkCore;
using AdvancedApi.Data;

namespace AdvancedApi;

internal static class MigrationExtensions
{
    public static IHost ApplyTodoMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
        context.Database.Migrate();
        return host;
    }
}