// https://learn.microsoft.com/en-us/azure/foundry-local/get-started?tabs=xplatform&pivots=programming-language-csharp

using FoundryLocal.Shared;
using Microsoft.AI.Foundry.Local;

FoundryLocalManager mgr = await FoundryLocalManagerFactory.MakeAsync<Program>();

Console.WriteLine("Execution providers:");
EpInfo[] executionProviderInfoList = mgr.DiscoverEps();
foreach (EpInfo executionProviderInfo in executionProviderInfoList)
{
    Console.WriteLine($"  - {executionProviderInfo.Name}");
}

ICatalog catalog = await mgr.GetCatalogAsync();

Console.WriteLine();
Console.WriteLine("Cached Models --------------");

List<IModel> cachedModels = await catalog.GetCachedModelsAsync();
foreach (IModel model in cachedModels)
{
    Console.WriteLine($"{model.Alias}");
}

Console.WriteLine();
Console.WriteLine("Model Catalog --------------");

List<IModel> models = await catalog.ListModelsAsync();
foreach (var model in models)
{
    Console.WriteLine($"{model.Alias}");
    foreach (IModel variant in model.Variants)
    {
        Console.WriteLine($"\t{nameof(variant.Info.Name)}: {variant.Info.Name}");
        Console.WriteLine($"\t\t{nameof(variant.Info.Capabilities)}:\t\t{variant.Info.Capabilities}");
        Console.WriteLine($"\t\t{nameof(variant.Info.FileSizeMb)}:\t\t{variant.Info.FileSizeMb}");
        Console.WriteLine($"\t\t{nameof(variant.Info.MaxOutputTokens)}:\t\t{variant.Info.MaxOutputTokens}");
        Console.WriteLine($"\t\t{nameof(variant.Info.ModelType)}:\t\t{variant.Info.ModelType}");
        Console.WriteLine($"\t\t{nameof(variant.Info.OutputModalities)}:\t\t{variant.Info.OutputModalities}");
        Console.WriteLine($"\t\t{nameof(variant.Info.ProviderType)}:\t\t{variant.Info.ProviderType}");
        Console.WriteLine($"\t\t{nameof(variant.Info.Publisher)}:\t\t{variant.Info.Publisher}");
        Console.WriteLine($"\t\t{nameof(variant.Info.SupportsToolCalling)}:\t\t{variant.Info.SupportsToolCalling}");
        Console.WriteLine($"\t\t{nameof(variant.Info.Task)}:\t\t{variant.Info.Task}");
    }
}

Console.WriteLine("Press any key to exit...");
_ = Console.ReadKey();
