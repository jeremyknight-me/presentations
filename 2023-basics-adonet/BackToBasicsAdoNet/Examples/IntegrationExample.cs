using BackToBasicsAdoNet.EntityFramework.Chinook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class IntegrationExample
{
	internal static void Run(ConnectionStrings connectionStrings)
	{
		using var context = MakeContext(connectionStrings.Chinook);
		var connection = context.Database.GetDbConnection();
		using var command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = "SELECT TOP 20 ArtistId, Name FROM dbo.Artist;";

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		using var reader
			= command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
		while (reader.Read())
		{
			var id = reader.GetInt32("ArtistId");
			var name = reader.GetString("Name");
			Console.WriteLine($"Id = {id} | Name = {name}");
		}
	}

	private static ChinookContext MakeContext(string connectionString)
	{
		var builder = new DbContextOptionsBuilder<ChinookContext>();
		builder.UseSqlServer(connectionString);
		return new ChinookContext(builder.Options);
	}
}
