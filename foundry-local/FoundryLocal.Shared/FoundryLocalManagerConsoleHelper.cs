using Microsoft.AI.Foundry.Local;

namespace FoundryLocal.Shared;

public static class FoundryLocalManagerConsoleHelper
{
    public static async Task DownloadAndRegisterExecutionProvidersAsync(FoundryLocalManager mgr)
    {
        // Download and register all execution providers.
        var currentEp = "";
        await mgr.DownloadAndRegisterEpsAsync((epName, percent) =>
        {
            if (epName != currentEp)
            {
                if (currentEp != "")
                {
                    Console.WriteLine();
                }

                currentEp = epName;
            }
            Console.Write($"\r  {epName.PadRight(30)}  {percent,6:F1}%");
        });

        if (currentEp != "")
        {
            Console.WriteLine();
        }
    }
}
