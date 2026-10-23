namespace YtDownloader.Metadata;

public sealed class AudioCatalog
{
    private readonly IReadOnlyList<AudioFormat> formats;

    public AudioCatalog(IEnumerable<AudioFormat> formats)
    {
        this.formats = formats.ToList();
    }

    public AudioFormat? Original()
    {
        IReadOnlyList<AudioFormat> flagged = formats.Where(format => format.IsOriginal()).ToList();
        return Best(flagged.Count > 0 ? flagged : formats);
    }

    public AudioFormat? Dubbed(string language) =>
        Best(formats.Where(format => format.MatchesLanguage(language)));

    private static AudioFormat? Best(IEnumerable<AudioFormat> candidates) =>
        candidates.OrderByDescending(format => format.Bitrate).FirstOrDefault();
}
