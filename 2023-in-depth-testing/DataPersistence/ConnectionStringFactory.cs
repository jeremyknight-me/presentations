using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataPersistence;

public static class ConnectionStringFactory
{
	public static string Make()
	{
		var configBuilder = new ConfigurationBuilder();
		configBuilder.AddUserSecrets(typeof(SimpleContext).Assembly, true);
		var config = configBuilder.Build();

		var connectionString = "Server=.;Database=Simple;User Id=sa;Encrypt=True;TrustServerCertificate=True";
		var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
		{
			Password = config.GetValue<string>("DbPassword")
		};
		return connectionStringBuilder.ToString();
	}
}
