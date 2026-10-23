using System.Text.Json;
using System.Text.Json.Serialization;
using YtDownloader.Configuration;
using YtDownloader.Download;
using YtDownloader.Urls;

namespace YtDownloader.History;

public sealed class HistoryStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private readonly string path;

    public HistoryStore(AppSettings settings)
    {
        path = Path.Combine(settings.HistoryFolder, settings.HistoryFileName);
    }

    public void EnsureExists()
    {
        if (!File.Exists(path))
            Write(new List<HistoryEntry>());
    }

    public bool IsDone(VideoUrl url) =>
        Load().Any(entry => entry.Url == url.ToString() && entry.Status == DownloadStatus.Done);

    public IReadOnlyList<HistoryEntry> Unfinished() =>
        Load().Where(entry => entry.Status != DownloadStatus.Done).ToList();

    public void Record(VideoUrl url, DownloadResult result)
    {
        List<HistoryEntry> entries = Load();
        entries.RemoveAll(entry => entry.Url == url.ToString());
        entries.Add(HistoryEntry.From(url, result));
        Write(entries);
    }

    private List<HistoryEntry> Load() =>
        File.Exists(path)
            ? JsonSerializer.Deserialize<List<HistoryEntry>>(File.ReadAllText(path), Options) ?? new List<HistoryEntry>()
            : new List<HistoryEntry>();

    private void Write(List<HistoryEntry> entries)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, JsonSerializer.Serialize(entries, Options));
    }
}
