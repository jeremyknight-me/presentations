using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Transactions;

namespace BackToBasicsAdoNet.Examples
{
    public class UpdateExample
    {
        public void Run()
        {
            using (var scope = new TransactionScope())
            using (var connection = new SqlConnection(Settings.ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
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
                scope.Complete();
                Console.WriteLine($"{numRowAffected} rows updated");
            }
        }
    }
}
