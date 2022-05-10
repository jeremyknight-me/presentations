using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Transactions;

namespace BackToBasicsAdoNet.Examples;

public class InsertExample
{
    public void Run()
    {
        using (var scope = new TransactionScope())
        using (var connection = new SqlConnection(Settings.ConnectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandType = CommandType.Text; // or StoredProcedure
            command.CommandText = "INSERT INTO dbo.[Lookup](Name) VALUES (@name); SELECT SCOPE_IDENTITY();";

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
            scope.Complete();

            // Console.WriteLine handles a lot of additional parsing, checks, etc. that would normally be done.
            Console.WriteLine($"Id = {newId} | Name = {newName}");
        }
    }
}
