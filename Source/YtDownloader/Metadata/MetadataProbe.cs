using System.Text.Json;
using YtDownloader.Shell;
using YtDownloader.Urls;

namespace YtDownloader.Metadata;

public sealed class MetadataProbe
{
    private readonly CommandRunner runner;

    public MetadataProbe(CommandRunner runner)
    {
        this.runner = runner;
    }

    public VideoMetadata Fetch(VideoUrl url)
    {
        CommandResult result = runner.Capture("yt-dlp",
        [
            "-J",
            "--no-playlist",
            "--no-warnings",
            "--ignore-no-formats-error",
            "--encoding", "utf-8",
            url.ToString()
        ]);

        return result.IsSuccess()
            ? Parse(result.Output)
            : throw new InvalidOperationException($"yt-dlp could not read the video metadata (exit code {result.ExitCode})");
    }

    private static VideoMetadata Parse(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);
        return new VideoMetadata(document.RootElement.Clone());
    }
}
