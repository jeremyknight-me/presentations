using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataPersistence;

public sealed class SimpleContextDesignTimeFactory : IDesignTimeDbContextFactory<SimpleContext>
{
	public SimpleContext CreateDbContext(string[] args)
	{
		var connectionString = ConnectionStringFactory.Make();
		var builder = new DbContextOptionsBuilder<SimpleContext>();
		builder.UseSqlServer(connectionString);
		return new SimpleContext(builder.Options);
	}
}
