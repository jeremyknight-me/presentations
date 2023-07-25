using BackToBasicsAdoNet.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class IntegrationExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using var context = MakeContext(connectionStrings.Simple);
		var connection = context.Database.GetDbConnection();
		using var command = connection.CreateCommand();
		
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = "SELECT TOP 10 Id, Name FROM [dbo].[Lookups] WHERE IsDeleted = 0";

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		using var dataReader 
			= command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
		while (dataReader.Read())
		{
			var id = dataReader["Id"];
			var name = dataReader["Name"];
			// NOTE: ToString() handles a lot of additional parsing, checks, etc.
			// that would normally be done by the developer.
			Console.WriteLine($"Id = {id} | Name = {name}");
		}
	}

	private static SandboxContext MakeContext(string connectionString)
	{
		var builder = new DbContextOptionsBuilder<SandboxContext>();
		builder.UseSqlServer(connectionString);
		return new SandboxContext(builder.Options);
	}
}
