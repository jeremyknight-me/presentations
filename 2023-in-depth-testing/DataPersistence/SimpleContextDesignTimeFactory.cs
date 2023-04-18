using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataPersistence;

public sealed class SimpleContextDesignTimeFactory : IDesignTimeDbContextFactory<SimpleContext>
{
	public SimpleContext CreateDbContext(string[] args)
	{
		var configBuilder = new ConfigurationBuilder();
		configBuilder.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
		var config = configBuilder.Build();

		var connectionString = "Server=.;Database=Simple;User Id=sa;Encrypt=True;TrustServerCertificate=True";
		var conStrBuilder = new SqlConnectionStringBuilder(connectionString)
		{
			Password = config.GetValue<string>("DbPassword")
		};

		var builder = new DbContextOptionsBuilder<SimpleContext>();
		builder.UseSqlServer(conStrBuilder.ConnectionString);
		return new SimpleContext(builder.Options);
	}
}
