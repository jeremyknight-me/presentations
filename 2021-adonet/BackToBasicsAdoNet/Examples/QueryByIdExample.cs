using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples;

public class QueryByIdExample
{
    public void Run()
    {
        using (var connection = new SqlConnection(Settings.ConnectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandType = CommandType.Text; // or StoredProcedure
            command.CommandText = "SELECT Id, Name FROM [dbo].[Lookup] WHERE Id = @id";
            
            var idParameter = command.CreateParameter();
            idParameter.DbType = DbType.Int32;
            idParameter.Value = 2;
            idParameter.ParameterName = "@id";

            command.Parameters.Add(idParameter);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using (var dataReader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection))
            {
                while (dataReader.Read())
                {
                    var id = dataReader["Id"];
                    var name = dataReader["Name"];
                    // Console.WriteLine handles a lot of additional parsing, checks, etc. that would normally be done.
                    Console.WriteLine($"Id = {id} | Name = {name}");
                }
            }
        }
    }
}
