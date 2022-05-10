using Microsoft.Data.SqlClient;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

public class BulkInsertExample
{
    public void Run()
    {
        using (var connection = new SqlConnection(Settings.ConnectionString))
        using (var bulk = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.UseInternalTransaction, null))
        {
            bulk.BatchSize = 10;
            bulk.DestinationTableName = "Lookup";
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
}
