using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

public static class UpdateExample
{
    public static void Run()
    {
        using var connection = new SqlConnection(Settings.ConnectionString);
        using SqlCommand command = connection.CreateCommand();
        {
            command.CommandType = CommandType.Text; // or StoredProcedure
            command.CommandText = "UPDATE dbo.[Lookup] SET IsDeleted = 1 WHERE Id = @id;";

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
            Console.WriteLine($"{numRowAffected} rows updated");
        }
    }
}
