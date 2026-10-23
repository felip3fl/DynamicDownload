using YtDownloader.Configuration;
using YtDownloader.Download;
using YtDownloader.History;
using YtDownloader.Urls;

namespace YtDownloader.Workflow;

public sealed class DownloadWorkflow
{
    private readonly VideoDownloader downloader;
    private readonly HistoryStore history;
    private readonly AppSettings settings;

    public DownloadWorkflow(VideoDownloader downloader, HistoryStore history, AppSettings settings)
    {
        this.downloader = downloader;
        this.history = history;
        this.settings = settings;
    }

    public DownloadStatus Run(VideoUrl url, int quality)
    {
        history.EnsureExists();
        return history.IsDone(url) ? SkipDuplicate(url) : Download(url, quality);
    }

    private static DownloadStatus SkipDuplicate(VideoUrl url)
    {
        Console.WriteLine($"Already downloaded, skipping: {url}");
        return DownloadStatus.Done;
    }

    private DownloadStatus Download(VideoUrl url, int quality)
    {
        DownloadResult result = downloader.Download(url, quality);
        history.Record(url, result);
        Announce(result);

        if (result.IsDone())
            RetryUnfinished();

        return result.Status;
    }

    private static void Announce(DownloadResult result)
    {
        if (result.Status == DownloadStatus.Scheduled)
            Console.WriteLine($"Video not released yet. Release date: {result.ReleaseDate?.ToString("g") ?? "unknown"}. Saved to history.");
    }

    private void RetryUnfinished()
    {
        foreach (HistoryEntry entry in history.Unfinished())
            Retry(entry);
    }

    private void Retry(HistoryEntry entry)
    {
        if (!VideoUrl.TryParse(entry.OriginalUrl, out VideoUrl? url))
            return;

        Console.WriteLine($"Retrying unfinished video from history: {entry.Url}");
        history.Record(url!, downloader.Download(url!, settings.DefaultQuality));
    }
}
