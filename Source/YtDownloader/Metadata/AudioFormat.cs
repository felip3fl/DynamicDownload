namespace YtDownloader.Metadata;

public sealed record AudioFormat(string Id, string Language, string Note, double Bitrate)
{
    public bool IsOriginal() => Note.Contains("original", StringComparison.OrdinalIgnoreCase);

    public bool MatchesLanguage(string language) =>
        Language.Split('-')[0].Equals(language, StringComparison.OrdinalIgnoreCase);
}
