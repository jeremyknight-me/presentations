using System;
using BackToBasicsAdoNet.Examples;
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

            if (!enabled) { QueryExample.Run(connectionStrings); }
            if (!enabled) { QueryByIdExample.Run(connectionStrings); }
            
            if (!enabled) { InsertExample.Run(connectionStrings); }
            if (!enabled) { BulkCopyExample.Run(connectionStrings); }
            
            if (!enabled) { UpdateExample.Run(connectionStrings); }
            if (!enabled) { DeleteExample.Run(connectionStrings); }
            
            if (!enabled) { IntegrationExample.Run(connectionStrings); }

            if (!enabled) { BulkInsertExample.Run(connectionStrings); }
            if (!enabled) { QueryMultipleExample.Run(connectionStrings); }
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
