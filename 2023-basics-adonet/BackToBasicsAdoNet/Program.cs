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

            var enabled = true;
            if (enabled) { Examples.QueryExample.Run(connectionStrings); }
            if (!enabled) { Examples.QueryByIdExample.Run(connectionStrings); }
            if (!enabled) { Examples.InsertExample.Run(connectionStrings); }
            if (!enabled) { Examples.UpdateExample.Run(connectionStrings); }
            if (!enabled) { Examples.DeleteExample.Run(connectionStrings); }
            if (!enabled) { Examples.BulkInsertExample.Run(connectionStrings); }
            if (!enabled) { Examples.IntegrationExample.Run(connectionStrings); }
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
