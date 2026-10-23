using YtDownloader.Download;
using YtDownloader.Urls;

namespace YtDownloader.History;

public sealed record HistoryEntry(
    string OriginalUrl,
    string Url,
    string? Title,
    string? Channel,
    DateTimeOffset DownloadDate,
    DownloadStatus Status,
    DateTimeOffset? ReleaseDate)
{
    public static HistoryEntry From(VideoUrl url, DownloadResult result) =>
        new(url.Original(), url.ToString(), result.Title, result.Channel, DateTimeOffset.Now, result.Status, result.ReleaseDate);
}
