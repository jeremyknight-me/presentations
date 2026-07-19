using Microsoft.AI.Foundry.Local;

namespace FoundryLocal.Shared;

public static class ModelLoader
{
    public static async Task<IModel> LoadAsync(ICatalog catalog, string modelAlias)
    {
        IModel model = await catalog.GetModelAsync(modelAlias)
            ?? throw new Exception($"Model '{modelAlias}' not found");

        await model.DownloadAsync(progress =>
        {
            Console.Write($"\rDownloading model '{modelAlias}': {progress:F2}%");
            if (progress >= 100f) Console.WriteLine();
        });

        await model.LoadAsync();
        Console.WriteLine($"Model '{modelAlias}' loaded and ready.\n");
        return model;
    }
}
