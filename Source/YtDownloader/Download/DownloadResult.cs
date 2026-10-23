using YtDownloader.Metadata;

namespace YtDownloader.Download;

public sealed record DownloadResult(DownloadStatus Status, string? Title, string? Channel, DateTimeOffset? ReleaseDate)
{
    public static DownloadResult Done(VideoMetadata metadata) =>
        new(DownloadStatus.Done, metadata.Title(), metadata.Channel(), null);

    public static DownloadResult Scheduled(VideoMetadata metadata) =>
        new(DownloadStatus.Scheduled, metadata.Title(), metadata.Channel(), metadata.ReleaseDate());

    public static DownloadResult Failed(VideoMetadata? metadata) =>
        new(DownloadStatus.Failed, metadata?.Title(), metadata?.Channel(), null);

    public bool IsFailed() => Status == DownloadStatus.Failed;

    public bool IsDone() => Status == DownloadStatus.Done;
}
