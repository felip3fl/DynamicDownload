namespace YtDownloader.Shell;

public sealed record CommandResult(int ExitCode, string Output)
{
    public bool IsSuccess() => ExitCode == 0;
}
