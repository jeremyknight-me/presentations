using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using System;
using System.Linq;
using BackToBasicsAdoNet.EntityFramework.Simple;
using System.Text.Json;

namespace BackToBasicsAdoNet.Examples;

internal static class BulkInsertExample
{
	internal static void Run(ConnectionStrings connectionStrings)
	{
		using var scope = new TransactionScope();
		using var connection = new SqlConnection(connectionStrings.Simple);
		using SqlCommand command = connection.CreateCommand();
		command.CommandType = CommandType.Text; // or StoredProcedure
		command.CommandText = MakeCommandText();

		command.Parameters.Add("@json", SqlDbType.NVarChar);
		command.Parameters["@json"].Value = MakeParameterValue();

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		var numRowAffected = command.ExecuteNonQuery();
		scope.Complete();
		Console.WriteLine($"{numRowAffected} rows inserted");
	}

	private static string MakeParameterValue()
	{
		var lookups = Enumerable
			.Range(1, 10)
			.Select(x => new Lookup { Name = $"Bulk Inserted {x}", IsDeleted = x % 2 == 0 });
		var json = JsonSerializer.Serialize(lookups);
		return json;
	}

	private static string MakeCommandText() => 
		"""
		INSERT INTO dbo.Lookups ([Name], IsDeleted)
		SELECT 
			j.[Name],
			j.IsDeleted
		FROM 
		OPENJSON (@json, '$')
		WITH 
		(
			[Name] NVARCHAR(50) '$.Name',
			IsDeleted bit '$.IsDeleted'
		) AS j;
		""";
}
