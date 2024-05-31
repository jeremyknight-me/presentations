using System;

namespace BackToBasicsAdoNet;

internal class Program
{
    private static void Main()
    {
        try
        {
#if DEBUG
            ExamplesManager.Run();
#endif
#if RELEASE
            BenchmarksManager.Run();
#endif
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
