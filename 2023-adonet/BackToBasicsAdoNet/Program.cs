using System;

namespace BackToBasicsAdoNet;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            new Examples.QueryExample().Run();
            //new Examples.QueryByIdExample().Run();
            //new Examples.InsertExample().Run();
            //new Examples.UpdateExample().Run();
            //new Examples.DeleteExample().Run();
            //new Examples.BulkInsertExample().Run();
            //new Examples.IntegrationExample().Run();
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
