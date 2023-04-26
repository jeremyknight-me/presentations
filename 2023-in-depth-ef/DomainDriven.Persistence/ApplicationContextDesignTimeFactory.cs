using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DomainDriven.Persistence;

public class ApplicationContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var connectionString = ConnectionStringFactory.Make();
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseSqlServer(connectionString);
        return new ApplicationContext(builder.Options);
    }
}
