using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BackToBasicsAdoNet.Examples;

internal static class QueryMultipleExample
{
	internal static void Run(ConnectionStrings connectionStrings)
	{
		using var connection = new SqlConnection(connectionStrings.Chinook);
		using var command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText =
			"""
			SELECT GenreId, Name FROM dbo.Genre;
			SELECT MediaTypeId, Name FROM dbo.MediaType;
			""";

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		using var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
		while (reader.HasRows)
		{
			Console.WriteLine("ID | Name");
			Console.WriteLine("---------");

			while (reader.Read())
			{
				var id = reader.GetInt32(0);
				var name = reader.GetString(1);
				Console.WriteLine($"Id = {id} | Name = {name}");
			}
            Console.WriteLine();
            reader.NextResult();
		}
	}
}
