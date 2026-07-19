// https://learn.microsoft.com/en-us/azure/foundry-local/tutorials/tutorial-build-tool-calling-assistant?tabs=xplatform&pivots=programming-language-csharp

using System.Text.Json;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using FoundryLocal.Shared;
using Microsoft.AI.Foundry.Local;
using ToolCallAssistant;

FoundryLocalManager mgr = await FoundryLocalManagerFactory.MakeAsync<Program>();
await FoundryLocalManagerConsoleHelper.DownloadAndRegisterExecutionProvidersAsync(mgr);

ICatalog catalog = await mgr.GetCatalogAsync();
IModel model = await ModelLoader.LoadAsync(catalog, ModelAliases.Qwen2_5._5b);

OpenAIChatClient chatClient = await model.GetChatClientAsync();
chatClient.Settings.ToolChoice = ToolChoice.Auto;

List<ChatMessage> messages =
    [
        new()
        {
            Role = "system",
            Content = "You are a helpful assistant with access to tools. " +
                      "Use them when needed to answer questions accurately."
        }
    ];

Console.WriteLine("\nTool-calling assistant ready! Type 'quit' to exit.\n");

List<ToolDefinition> tools = ToolDefinitions.Build();
while (true)
{
    Console.Write("You: ");
    var userInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(userInput) ||
        userInput.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
        userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    messages.Add(new ChatMessage
    {
        Role = "user",
        Content = userInput
    });

    var response = await chatClient.CompleteChatAsync(messages, tools, default);
    ChatMessage choice = response.Choices[0].Message;

    if (choice.ToolCalls is { Count: > 0 })
    {
        messages.Add(choice);

        foreach (ToolCall toolCall in choice.ToolCalls)
        {
            JsonElement toolArgs = JsonDocument.Parse(toolCall.FunctionCall.Arguments).RootElement;
            Console.WriteLine(
                $"  Tool call: {toolCall.FunctionCall.Name}({toolArgs})"
            );

            var result = ToolRunner.Run(toolCall.FunctionCall.Name, toolArgs);
            messages.Add(new ChatMessage
            {
                Role = "tool",
                ToolCallId = toolCall.Id,
                Content = result
            });
        }

        var finalResponse = await chatClient.CompleteChatAsync(messages, tools, default);
        var answer = finalResponse.Choices[0].Message.Content ?? "";
        messages.Add(new ChatMessage
        {
            Role = "assistant",
            Content = answer
        });
        Console.WriteLine($"Assistant: {answer}\n");
    }
    else
    {
        var answer = choice.Content ?? "";
        messages.Add(new ChatMessage
        {
            Role = "assistant",
            Content = answer
        });
        Console.WriteLine($"Assistant: {answer}\n");
    }
}

await model.UnloadAsync();
Console.WriteLine("Model unloaded. Goodbye!");
Console.WriteLine("Press any key to exit...");
_ = Console.ReadKey();