using Microsoft.AI.Foundry.Local;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using FoundryLocal.Shared;

FoundryLocalManager mgr = await FoundryLocalManagerFactory.MakeAsync<Program>();
await FoundryLocalManagerConsoleHelper.DownloadAndRegisterExecutionProvidersAsync(mgr);

// Select and load a model from the catalog
ICatalog catalog = await mgr.GetCatalogAsync();
IModel model = await ModelLoader.LoadAsync(catalog, ModelAliases.Qwen2_5._5b);

// Get a chat client
OpenAIChatClient chatClient = await model.GetChatClientAsync();

// Start the conversation with a system prompt
List<ChatMessage> messages =
    [
        new ChatMessage
        {
            Role = "system",
            Content = "You are a helpful, friendly assistant. Keep your responses " +
                      "concise and conversational. If you don't know something, say so."
        }
    ];

Console.WriteLine("\nChat assistant ready! Type 'quit' to exit.\n");

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

    // Add the user's message to conversation history
    messages.Add(new ChatMessage { Role = "user", Content = userInput });

    // Stream the response token by token
    Console.Write("Assistant: ");
    var fullResponse = string.Empty;
    var streamingResponse = chatClient.CompleteChatStreamingAsync(messages, default);
    await foreach (var chunk in streamingResponse)
    {
        var content = chunk.Choices[0].Message.Content;
        if (!string.IsNullOrEmpty(content))
        {
            Console.Write(content);
            Console.Out.Flush();
            fullResponse += content;
        }
    }
    Console.WriteLine("\n");

    // Add the complete response to conversation history
    messages.Add(new ChatMessage { Role = "assistant", Content = fullResponse });
}

// Clean up - unload the model
await model.UnloadAsync();
Console.WriteLine("Model unloaded. Goodbye!");
Console.WriteLine("Press any key to exit...");
_ = Console.ReadKey();