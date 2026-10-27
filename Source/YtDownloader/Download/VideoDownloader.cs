using YtDownloader.Configuration;
using YtDownloader.History;
using YtDownloader.Metadata;
using YtDownloader.Shell;
using YtDownloader.Urls;

namespace YtDownloader.Download;

public sealed class VideoDownloader
{
    private readonly AppSettings settings;
    private readonly CommandRunner runner;
    private readonly HistoryStore history;

    public VideoDownloader(AppSettings settings, CommandRunner runner, HistoryStore history)
    {
        this.settings = settings;
        this.runner = runner;
        this.history = history;
    }

    public DownloadResult Download(VideoUrl url, int quality)
    {
        DownloadResult result = DownloadResult.Failed(null);

        for (int attempt = 1; attempt <= settings.MaxAttempts && result.IsFailed(); attempt++)
            result = TryOnce(url, quality, attempt);

        return result;
    }

    private DownloadResult TryOnce(VideoUrl url, int quality, int attempt)
    {
        Console.WriteLine($"Attempt {attempt} of {settings.MaxAttempts}: {url}");
        try
        {
            return Execute(url, quality);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Attempt {attempt} failed: {exception.Message}");
            return DownloadResult.Failed(null);
        }
    }

    private DownloadResult Execute(VideoUrl url, int quality)
    {
        VideoMetadata metadata = new MetadataProbe(runner).Fetch(url);
        if (metadata.IsUpcoming())
            return DownloadResult.Scheduled(metadata);
        history.RecordDownloading(url, metadata);
        return Fetch(url, metadata, quality);
    }

    private DownloadResult Fetch(VideoUrl url, VideoMetadata metadata, int quality)
    {
        string folder = Path.Combine(settings.TempFolder, $"yt-dlp-{metadata.Id()}");
        new AvailabilityReport(metadata, settings).Print();
        int exitCode = runner.Run("yt-dlp", new DownloadArguments(settings, metadata, quality).Build(url, folder));
        return exitCode == 0 ? Finish(folder, metadata) : DownloadResult.Failed(metadata);
    }

    private DownloadResult Finish(string folder, VideoMetadata metadata)
    {
        new FileMover(settings.DestinationFolder).Move(folder);
        return DownloadResult.Done(metadata);
    }
}
