using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleApi.Data;

public sealed class TodoContextDesignTimeFactory : IDesignTimeDbContextFactory<TodoContext>
{
    public TodoContext CreateDbContext(string[] args)
    {
        SqliteConnectionStringBuilder connectionStringBuilder = new() { DataSource = "foo.dat" };
        DbContextOptionsBuilder<TodoContext> optionsBuilder = new();
        optionsBuilder.UseSqlite(connectionStringBuilder.ConnectionString);
        return new TodoContext(optionsBuilder.Options);
    }
}