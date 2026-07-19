// https://learn.microsoft.com/en-us/azure/foundry-local/tutorials/tutorial-build-voice-to-text-note-taker?tabs=xplatform&pivots=programming-language-csharp

using Microsoft.AI.Foundry.Local;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using System.Text;
using FoundryLocal.Shared;

FoundryLocalManager mgr = await FoundryLocalManagerFactory.MakeAsync<Program>();
await FoundryLocalManagerConsoleHelper.DownloadAndRegisterExecutionProvidersAsync(mgr);

ICatalog catalog = await mgr.GetCatalogAsync();
IModel speechModel = await ModelLoader.LoadAsync(catalog, ModelAliases.Whisper.Small);

// Transcribe the audio file
OpenAIAudioClient audioClient = await speechModel.GetAudioClientAsync();
StringBuilder transcriptionText = new();

Console.WriteLine("\nTranscription:");
const string audioFilePath = @"C:\Users\Jeremy\Downloads\audio-samples\JonesMedical.mp3";

var audioResponse = audioClient
    .TranscribeAudioStreamingAsync(audioFilePath, default);
await foreach (var chunk in audioResponse)
{
    Console.Write(chunk.Text);
    transcriptionText.Append(chunk.Text);
}
Console.WriteLine();

// Unload the speech model to free memory
await speechModel.UnloadAsync();

// Load the chat model for summarization
IModel chatModel = await ModelLoader.LoadAsync(catalog, ModelAliases.Qwen2_5._5b);

// Summarize the transcription into organized notes
OpenAIChatClient chatClient = await chatModel.GetChatClientAsync();
List<ChatMessage> messages =
    [
        new ChatMessage
        {
            Role = "system",
            Content = "You are a note-taking assistant. Summarize " +
                      "the following transcription into organized, " +
                      "concise notes with bullet points."
        },
        new ChatMessage
        {
            Role = "user",
            Content = transcriptionText.ToString()
        }
    ];

var chatResponse = await chatClient.CompleteChatAsync(messages, default);
var summary = chatResponse.Choices[0].Message.Content;
Console.WriteLine($"\nSummary:\n{summary}");

// Clean up
await chatModel.UnloadAsync();
Console.WriteLine("\nDone. Models unloaded.");