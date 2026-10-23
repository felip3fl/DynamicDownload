using YtDownloader.Metadata;

namespace YtDownloader.Download;

public sealed class SubtitleArguments
{
    private readonly SubtitleCatalog catalog;
    private readonly IEnumerable<string> languages;

    public SubtitleArguments(SubtitleCatalog catalog, IEnumerable<string> languages)
    {
        this.catalog = catalog;
        this.languages = languages;
    }

    public IEnumerable<string> Build()
    {
        string[] available = languages.Select(catalog.Find).OfType<string>().ToArray();

        List<String> availableWithAuto = new();

        foreach (var item in available)
        {
            availableWithAuto.Add(item + ".*");
        }

        return availableWithAuto.Count == 0
            ? Array.Empty<string>()
            : new[] { "--write-auto-sub", "--sub-langs", string.Join(',', availableWithAuto),"-i" };
    }
}
