using YtDownloader.Metadata;

namespace YtDownloader.Download;

public sealed class FormatExpression
{
    private readonly int quality;
    private readonly AudioCatalog audio;
    private readonly string dubbingLanguage;

    public FormatExpression(int quality, AudioCatalog audio, string dubbingLanguage)
    {
        this.quality = quality;
        this.audio = audio;
        this.dubbingLanguage = dubbingLanguage;
    }

    public string Build() =>
        $"bv[height<={quality}]{AudioSelection()}" +
        $"/bv*[height<={quality}]+ba" +
        $"/b[height<={quality}]" +
        "/bv*+ba/b";

    private string AudioSelection()
    {
        IEnumerable<string> ids = new[] { audio.Original()?.Id, audio.Dubbed(dubbingLanguage)?.Id }
            .OfType<string>()
            .Distinct();

        string selection = string.Concat(ids.Select(id => $"+{id}"));
        return selection.Length > 0 ? selection : "+ba";
    }
}
