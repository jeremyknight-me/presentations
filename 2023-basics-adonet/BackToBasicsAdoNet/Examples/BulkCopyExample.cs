using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

internal static class BulkCopyExample
{
	internal static void Run(ConnectionStrings connectionStrings)
    {
		using var connection = new SqlConnection(connectionStrings.Simple);
		using var bulk = new SqlBulkCopy(
			connection,
			SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.UseInternalTransaction,
			null);
		bulk.BatchSize = 10;
		bulk.DestinationTableName = "Lookups";
 		bulk.ColumnMappings.Add(
			sourceColumn: "Name", // column name in the DataTable configured below
			destinationColumn: "Name" // column name in the database table
		); 

		var table = new DataTable();
		table.Columns.Add("Name", typeof(string));

		for (int i = 0; i < 10; i++)
		{
			var row = table.NewRow();
			row["Name"] = $"Bulk Copied {i + 1} | {DateTime.Now.Ticks}";
			table.Rows.Add(row);
		}

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		bulk.WriteToServer(table);
	}
}
