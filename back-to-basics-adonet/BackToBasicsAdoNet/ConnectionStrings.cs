using Microsoft.Extensions.Configuration;

namespace BackToBasicsAdoNet;

internal sealed class ConnectionStrings
{
	public required string Chinook { get; init; }
	public required string Simple { get; init; }

	public static ConnectionStrings Create()
	{
		var configBuilder = new ConfigurationBuilder();
		configBuilder.AddUserSecrets(typeof(Program).Assembly, true);
		var config = configBuilder.Build();
		return new ConnectionStrings
		{
			Chinook = config.GetConnectionString(nameof(Chinook)),
			Simple = config.GetConnectionString(nameof(Simple))
		};
	}
}
