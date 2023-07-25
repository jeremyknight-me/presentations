using System;
using Microsoft.Extensions.Configuration;

namespace BackToBasicsAdoNet;

internal class Program
{
    private static void Main()
    {
        try
        {
			var configBuilder = new ConfigurationBuilder();
			configBuilder.AddUserSecrets(typeof(Program).Assembly, true);
			var config = configBuilder.Build();
            var connectionStrings = new ConnectionStrings
            {
                Chinook = config.GetConnectionString(nameof(ConnectionStrings.Chinook)),
                Simple = config.GetConnectionString(nameof(ConnectionStrings.Simple))
			};

			Examples.QueryExample.Run(connectionStrings);
            Examples.QueryByIdExample.Run(connectionStrings);
            //Examples.InsertExample.Run(connectionStrings);
            //Examples.UpdateExample.Run(connectionStrings);
            //Examples.DeleteExample.Run(connectionStrings);
            //Examples.BulkInsertExample.Run(connectionStrings);
            Examples.IntegrationExample.Run(connectionStrings);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to exit:");
            Console.ReadKey();
        }
    }
}
