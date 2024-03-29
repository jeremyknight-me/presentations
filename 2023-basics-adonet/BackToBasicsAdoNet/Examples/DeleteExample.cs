﻿using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class DeleteExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using var connection = new SqlConnection(connectionStrings.Simple);
		using SqlCommand command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = "DELETE FROM dbo.[Lookups] WHERE Id = @id;";

		var idParameter = command.CreateParameter();
		idParameter.DbType = DbType.Int32;
		idParameter.Value = 5;
		idParameter.ParameterName = "@id";

		command.Parameters.Add(idParameter);

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		var numRowAffected = command.ExecuteNonQuery();
		Console.WriteLine($"{numRowAffected} rows deleted");
	}
}
