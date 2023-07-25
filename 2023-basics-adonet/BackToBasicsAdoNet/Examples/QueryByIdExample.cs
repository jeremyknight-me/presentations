using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class QueryByIdExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using (var connection = new SqlConnection(connectionStrings.Simple))
		using (var command = connection.CreateCommand())
		{
			command.CommandType = CommandType.Text; // or StoredProcedure
			command.CommandText = "SELECT Id, Name FROM [dbo].[Lookups] WHERE Id = @id";

			var idParameter = command.CreateParameter();
			idParameter.DbType = DbType.Int32;
			idParameter.Value = 2;
			idParameter.ParameterName = "@id";

			command.Parameters.Add(idParameter);

			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			using (var reader
				= command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection))
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
