using BackToBasicsAdoNet.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace BackToBasicsAdoNet.Examples
{
    public class IntegrationExample
    {
        public void Run()
        {
            var builder = new DbContextOptionsBuilder<SandboxContext>();
            builder.UseSqlServer(Settings.ConnectionString);
            using (var context = new SandboxContext(builder.Options))
            {
                var connection = context.Database.GetDbConnection(); 
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text; // or StoredProcedure
                    command.CommandText = "SELECT Id, Name FROM [dbo].[Lookup] WHERE IsDeleted = 0";

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (var dataReader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
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
    }
}
