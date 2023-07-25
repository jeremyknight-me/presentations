using Microsoft.Data.SqlClient;
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
		bulk.ColumnMappings.Add("Name", "Name"); // source, destination

		var table = new DataTable();
		table.Columns.Add("Name", typeof(string));

		var seed = -9999;
		for (int i = 0; i < 10; i++)
		{
			var row = table.NewRow();
			row["Name"] = $"Bulk Inserted {seed++}";
			table.Rows.Add(row);
		}

		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}

		bulk.WriteToServer(table);
	}
}
