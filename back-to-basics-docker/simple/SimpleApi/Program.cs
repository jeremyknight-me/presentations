using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

namespace SimpleApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TodoContext>(
            options => options.UseSqlite(builder.Configuration.GetConnectionString("Todos")));

        var app = builder.Build();

        app.MapEndpoints();
        app.ApplyTodoMigrations();
        app.Run();
    }
}
