using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels.SharedModels;

namespace ToolCallAssistant;

internal static class ToolDefinitions
{
    internal static List<ToolDefinition> Build()
    {
        return [
            GetWeather(),
            Calculate()
        ];
    }

    private static ToolDefinition Calculate() 
        => new()
        {
            Type = "function",
            Function = new FunctionDefinition()
            {
                Name = "calculate",
                Description = "Perform a math calculation",
                Parameters = new PropertyDefinition()
                {
                    Type = "object",
                    Properties = new Dictionary<string, PropertyDefinition>()
                    {
                        { "expression", new PropertyDefinition() { Type = "string", Description = "The math expression to evaluate" } }
                    },
                    Required = ["expression"]
                }
            }
        };

    private static ToolDefinition GetWeather() 
        => new()
        {
            Type = "function",
            Function = new FunctionDefinition()
            {
                Name = "get_weather",
                Description = "Get the current weather for a location",
                Parameters = new PropertyDefinition()
                {
                    Type = "object",
                    Properties = new Dictionary<string, PropertyDefinition>()
                    {
                        { "location", new PropertyDefinition() { Type = "string", Description = "The city or location" } },
                        { "unit", new PropertyDefinition() { Type = "string", Description = "Temperature unit (celsius or fahrenheit)" } }
                    },
                    Required = ["location"]
                }
            }
        };
}
