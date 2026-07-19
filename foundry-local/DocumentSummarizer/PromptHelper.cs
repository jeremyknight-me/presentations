
namespace DocumentSummarizer;

internal static class PromptHelper
{
    internal static string BuildSystemPrompt(string[] args)
    {
        if (args.Length > 1)
        {
            return ResolvePrompt(args[1]);
        }

        Console.WriteLine("Select a summarization style:");
        Console.WriteLine("1. bullet points");
        Console.WriteLine("2. one paragraph summary");
        Console.WriteLine("3. key takeaways");

        var selection = Console.ReadLine();
        return ResolvePrompt(selection ?? string.Empty);
    }

    private static string ResolvePrompt(string selection)
    {
        if (string.IsNullOrWhiteSpace(selection))
        {
            return CreateSystemPrompt("bullet points");
        }

        var normalized = selection.Trim();
        return normalized.ToLowerInvariant() switch
        {
            "1" => CreateSystemPrompt("bullet points"),
            "2" => CreateSystemPrompt("one paragraph summary"),
            "3" => CreateSystemPrompt("key takeaways"),
            _ => CreateSystemPrompt("bullet points")
        };
    }

    private static string CreateSystemPrompt(string style) 
        => style switch
        {
            "bullet points" =>
                "Summarize the following document into concise bullet points. "
                + "Focus on the key points and main ideas.",
            "one paragraph summary" =>
                "Summarize the following document in a single, concise paragraph. "
                + "Capture the main argument and supporting points.",
            "key takeaways" =>
                "Extract the three most important takeaways from the following document. "
                + "Number each takeaway and keep each to one or two sentences.",
            _ => "Summarize the following document."
        };
}
