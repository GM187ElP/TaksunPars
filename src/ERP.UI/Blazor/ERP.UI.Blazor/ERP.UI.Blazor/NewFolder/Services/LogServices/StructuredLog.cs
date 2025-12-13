using System.Text.Json;

namespace PersonellInfo.Blazor.Components.Services.LogServices;

public class StructuredLog
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public LogCategory Category { get; set; }
    public LogLevel Level { get; set; }
    public ErrorType ErrorType { get; set; } = ErrorType.None;
    public string Message { get; set; } = string.Empty;
    public object? Context { get; set; }

    public void LogStructured(StructuredLog log)
    {
        var json = JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}



public enum LogCategory { Api, Database, UI, Auth, BackgroundTask }
public enum LogLevel { Trace, Debug, Info, Warning, Error, Critical }
public enum ErrorType { None, Validation, Unauthorized, Network, Timeout, Unexpected }
