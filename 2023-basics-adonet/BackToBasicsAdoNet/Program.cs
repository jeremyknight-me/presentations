using System;

namespace BackToBasicsAdoNet;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Examples.QueryExample.Run();
            Examples.QueryByIdExample.Run();
            Examples.InsertExample.Run();
            Examples.UpdateExample.Run();
            Examples.DeleteExample.Run();
            Examples.BulkInsertExample.Run();
            Examples.IntegrationExample.Run();
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
