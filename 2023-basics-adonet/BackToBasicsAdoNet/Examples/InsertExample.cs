using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class InsertExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using var connection = new SqlConnection(connectionStrings.Simple);
		using SqlCommand command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = "INSERT INTO dbo.[Lookups](Name) VALUES (@name); SELECT SCOPE_IDENTITY();";

		var newName = "The New Guy";
		var nameParameter = command.CreateParameter();
		nameParameter.DbType = DbType.String;
		nameParameter.Value = newName;
		nameParameter.ParameterName = "@name";

		command.Parameters.Add(nameParameter);

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		var newId = command.ExecuteScalar();
		
		// Console.WriteLine handles a lot of additional parsing, checks, etc. that would normally be done.
		Console.WriteLine($"Id = {newId} | Name = {newName}");
	}
}
