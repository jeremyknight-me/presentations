using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdvancedApi.Data;

public sealed class TodoContextDesignTimeFactory : IDesignTimeDbContextFactory<TodoContext>
{
    public TodoContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<TodoContext> builder = new();
        builder.UseNpgsql(connectionString: "Host=localhost;Database=Garage;Username=postgres;Password=changeme;");
        return new TodoContext(builder.Options);
    }
}
