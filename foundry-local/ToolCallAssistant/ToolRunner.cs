using System.Text.Json;

namespace ToolCallAssistant;

internal static class ToolRunner
{
    internal static string Run(string functionName, JsonElement arguments)
    {
        switch (functionName)
        {
            case "get_weather":
                return RunGetWeather(arguments);
            case "calculate":
                return RunCalculate(arguments);
            default:
                return JsonSerializer.Serialize(new 
                { 
                    error = $"Unknown function: {functionName}" 
                });
        }
    }

    private static string RunGetWeather(JsonElement arguments)
    {
        var location = arguments.GetProperty("location").GetString() ?? "unknown";
        var unit = arguments.TryGetProperty("unit", out var u)
            ? u.GetString() ?? "celsius"
            : "celsius";
        var temp = unit == "celsius" ? 22 : 72;
        return JsonSerializer.Serialize(new
        {
            location,
            temperature = temp,
            unit,
            condition = "Sunny"
        });
    }

    private static string RunCalculate(JsonElement arguments)
    {
        var expression = arguments.GetProperty("expression").GetString() ?? "";
        try
        {
            var result = new System.Data.DataTable()
                .Compute(expression, null);
            return JsonSerializer.Serialize(new
            {
                expression,
                result = result?.ToString()
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                error = ex.Message
            });
        }
    }
}
