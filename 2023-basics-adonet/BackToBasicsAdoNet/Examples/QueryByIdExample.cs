using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class QueryByIdExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using (var connection = new SqlConnection(connectionStrings.Chinook))
		using (var command = connection.CreateCommand())
		{
			command.CommandType = CommandType.Text; // or StoredProcedure
			command.CommandText = "SELECT ArtistId, Name FROM dbo.Artist WHERE ArtistId = @id";

			var idParameter = command.CreateParameter();
			idParameter.DbType = DbType.Int32;
			idParameter.Value = 137;
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
					var id = reader.GetInt32("ArtistId");
					var name = reader.GetString("Name");
					Console.WriteLine($"Id = {id} | Name = {name}");
				}
			}
		}
			
	}
}
