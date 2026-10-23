namespace YtDownloader.Metadata;

public sealed class SubtitleCatalog
{
    private readonly IReadOnlyList<string> manual;
    private readonly IReadOnlyList<string> automatic;

    public SubtitleCatalog(IEnumerable<string> manual, IEnumerable<string> automatic)
    {
        this.manual = manual.ToList();
        this.automatic = automatic.ToList();
    }

    public string? Find(string language) =>
        Match(manual, language) ?? Match(automatic, language);

    public bool AnyAvailable(IEnumerable<string> languages) =>
        languages.Any(l => Find(l) is not null);

    private static string? Match(IEnumerable<string> keys, string language) =>
        keys.FirstOrDefault(key => key.Equals(language, StringComparison.OrdinalIgnoreCase))
        ?? keys.FirstOrDefault(key => key.StartsWith(language + "-", StringComparison.OrdinalIgnoreCase));
}
