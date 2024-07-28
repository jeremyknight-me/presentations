using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class InsertExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using var scope = TransactionScopeFactory.Create();
		using var connection = new SqlConnection(connectionStrings.Simple);
		using SqlCommand command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = "INSERT INTO dbo.Lookups(Name) VALUES (@name); SELECT SCOPE_IDENTITY();";

		var newName = "Inserted Record!";
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
		scope.Complete();
		Console.WriteLine($"Id = {newId} | Name = {newName}");
	}
}
