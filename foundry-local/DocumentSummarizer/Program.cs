// https://learn.microsoft.com/en-us/azure/foundry-local/tutorials/tutorial-build-document-summarizer?tabs=xplatform&pivots=programming-language-csharp

using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using DocumentSummarizer;
using FoundryLocal.Shared;
using Microsoft.AI.Foundry.Local;

FoundryLocalManager mgr = await FoundryLocalManagerFactory.MakeAsync<Program>();
await FoundryLocalManagerConsoleHelper.DownloadAndRegisterExecutionProvidersAsync(mgr);

// Select and load a model from the catalog
ICatalog catalog = await mgr.GetCatalogAsync();
IModel model = await ModelLoader.LoadAsync(catalog, ModelAliases.Qwen3._6b);

// Get a chat client
OpenAIChatClient chatClient = await model.GetChatClientAsync();

var target = args.Length > 0 ? args[0] : "document.txt";
string systemPrompt = PromptHelper.BuildSystemPrompt(args);

if (Directory.Exists(target))
{
    await SummarizeDirectoryAsync(chatClient, target, systemPrompt, default);
}
else
{
    Console.WriteLine($"--- {Path.GetFileName(target)} ---");
    await SummarizeFileAsync(chatClient, target, systemPrompt, default);
}

// Clean up
await model.UnloadAsync();
Console.WriteLine("\nModel unloaded. Done!");
Console.WriteLine("Press any key to exit...");
_ = Console.ReadKey();

async Task SummarizeFileAsync(
    dynamic client,
    string filePath,
    string prompt,
    CancellationToken token)
{
    var fileContent = await File.ReadAllTextAsync(filePath, token);
    List<ChatMessage> messages =
    [
        new() { Role = "system", Content = prompt },
        new() { Role = "user", Content = fileContent }
    ];

    var response = await client.CompleteChatAsync(messages, token);
    Console.WriteLine(response.Choices[0].Message.Content);
}

async Task SummarizeDirectoryAsync(
    dynamic client,
    string directory,
    string prompt,
    CancellationToken token)
{
    var txtFiles = Directory.GetFiles(directory, "*.txt")
        .OrderBy(f => f)
        .ToArray();

    if (txtFiles.Length == 0)
    {
        Console.WriteLine($"No .txt files found in {directory}");
        return;
    }

    foreach (var txtFile in txtFiles)
    {
        Console.WriteLine($"--- {Path.GetFileName(txtFile)} ---");
        await SummarizeFileAsync(client, txtFile, prompt, token);
        Console.WriteLine();
    }
}