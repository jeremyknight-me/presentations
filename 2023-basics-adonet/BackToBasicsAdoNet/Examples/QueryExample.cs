using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class QueryExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using (var connection = new SqlConnection(connectionStrings.Simple)) 
		using (var command = connection.CreateCommand())
		{
			command.CommandType = CommandType.Text; // or StoredProcedure
			command.CommandText = "SELECT TOP 10 Id, Name FROM [dbo].[Lookups] WHERE IsDeleted = 0";

			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			using (var reader
				= command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
			{
				while (reader.Read())
				{
					var id = reader["Id"];
					var name = reader["Name"];
					// NOTE: ToString() handles a lot of additional parsing, checks, etc.
					// that would normally be done by the developer.
					Console.WriteLine($"Id = {id} | Name = {name}");
				}
			}
		}
		
	}
}
