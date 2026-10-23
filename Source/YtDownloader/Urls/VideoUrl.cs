using System.Web;

namespace YtDownloader.Urls;

public sealed class VideoUrl
{
    private readonly string original;
    private readonly Uri cleaned;

    private VideoUrl(string original, Uri cleaned)
    {
        this.original = original;
        this.cleaned = cleaned;
    }

    public static bool TryParse(string? raw, out VideoUrl? url)
    {
        string text = raw?.Trim() ?? string.Empty;
        bool valid = Uri.TryCreate(text, UriKind.Absolute, out Uri? parsed) && IsWeb(parsed);
        url = valid ? new VideoUrl(text, Clean(parsed!)) : null;
        return valid;
    }

    public string Original() => original;

    public override string ToString() => cleaned.AbsoluteUri;

    private static bool IsWeb(Uri candidate) =>
        candidate.Scheme == Uri.UriSchemeHttp || candidate.Scheme == Uri.UriSchemeHttps;

    private static Uri Clean(Uri source)
    {
        string videoId = HttpUtility.ParseQueryString(source.Query)["v"] ?? string.Empty;
        string query = videoId.Length > 0 ? $"v={Uri.EscapeDataString(videoId)}" : string.Empty;
        return new UriBuilder(source) { Query = query, Fragment = string.Empty }.Uri;
    }
}
