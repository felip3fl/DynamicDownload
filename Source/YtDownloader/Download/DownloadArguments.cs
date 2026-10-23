using YtDownloader.Configuration;
using YtDownloader.Metadata;
using YtDownloader.Urls;

namespace YtDownloader.Download;

public sealed class DownloadArguments
{
    private readonly AppSettings settings;
    private readonly VideoMetadata metadata;
    private readonly int quality;

    public DownloadArguments(AppSettings settings, VideoMetadata metadata, int quality)
    {
        this.settings = settings;
        this.metadata = metadata;
        this.quality = quality;
    }

    public IReadOnlyList<string> Build(VideoUrl url, string folder)
    {
        List<string> arguments = new(settings.YtDlpArguments);
        arguments.AddRange(["-f", Format()]);
        arguments.AddRange(Subtitles());
        arguments.AddRange(["-P", folder, "-o", settings.TitleFormat, url.ToString()]);
        return arguments;
    }

    private string Format() =>
        new FormatExpression(quality, metadata.Audio(), settings.DubbingLanguage).Build();

    private IEnumerable<string> Subtitles() =>
        new SubtitleArguments(metadata.Subtitles(), settings.SubtitleLanguages).Build();
}
