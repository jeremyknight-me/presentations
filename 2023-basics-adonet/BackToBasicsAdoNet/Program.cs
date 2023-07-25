using System;
using BackToBasicsAdoNet.Examples;

namespace BackToBasicsAdoNet;

internal class Program
{
    private static void Main()
    {
        try
        {
            var connectionStrings = ConnectionStrings.Create();
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
