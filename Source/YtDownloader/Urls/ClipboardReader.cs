using YtDownloader.Shell;

namespace YtDownloader.Urls;

public sealed class ClipboardReader
{
    private readonly CommandRunner runner;

    public ClipboardReader(CommandRunner runner)
    {
        this.runner = runner;
    }

    public string Read()
    {
        CommandResult result = runner.Capture("powershell", ["-NoProfile", "-Command", "Get-Clipboard"]);
        return result.Output
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .FirstOrDefault() ?? string.Empty;
    }
}
