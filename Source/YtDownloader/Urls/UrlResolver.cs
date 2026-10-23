namespace YtDownloader.Urls;

public sealed class UrlResolver
{
    private readonly CommandLine commandLine;
    private readonly ClipboardReader clipboard;

    public UrlResolver(CommandLine commandLine, ClipboardReader clipboard)
    {
        this.commandLine = commandLine;
        this.clipboard = clipboard;
    }

    public VideoUrl Resolve()
    {
        string fromArguments = commandLine.Url();
        string raw = fromArguments.Length > 0 ? fromArguments : ReadClipboard();
        return VideoUrl.TryParse(raw, out VideoUrl? url) ? url! : throw new ArgumentException($"Invalid URL: '{raw}'");
    }

    private string ReadClipboard()
    {
        Console.WriteLine("No URL argument provided, reading the clipboard");
        return clipboard.Read();
    }
}
