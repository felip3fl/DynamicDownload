using System.Text.Json;

namespace YtDownloader.Configuration;

public sealed class SettingsLoader
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    private readonly string path;

    public SettingsLoader(string path)
    {
        this.path = path;
    }

    public AppSettings Load()
    {
        AppSettings settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(path), Options)
            ?? throw new InvalidOperationException($"Invalid settings file: {path}");

        return settings with
        {
            TempFolder = Environment.ExpandEnvironmentVariables(settings.TempFolder),
            DestinationFolder = Environment.ExpandEnvironmentVariables(settings.DestinationFolder),
            HistoryFolder = Environment.ExpandEnvironmentVariables(settings.HistoryFolder)
        };
    }
}
