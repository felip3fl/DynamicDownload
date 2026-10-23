namespace YtDownloader.Urls;

public sealed class CommandLine
{
    private const string QualityFlag = "--quality";

    private readonly string[] arguments;

    public CommandLine(string[] arguments)
    {
        this.arguments = arguments;
    }

    public string Url()
    {
        int flagIndex = Array.IndexOf(arguments, QualityFlag);
        IEnumerable<string> remaining = arguments.Where((_, position) => !IsQualityToken(position, flagIndex));
        return remaining.FirstOrDefault(argument => !string.IsNullOrWhiteSpace(argument)) ?? string.Empty;
    }

    public int Quality(int fallback)
    {
        int flagIndex = Array.IndexOf(arguments, QualityFlag);
        bool hasValue = flagIndex >= 0 && flagIndex + 1 < arguments.Length;
        return hasValue && int.TryParse(arguments[flagIndex + 1], out int quality) ? quality : fallback;
    }

    private static bool IsQualityToken(int position, int flagIndex) =>
        flagIndex >= 0 && (position == flagIndex || position == flagIndex + 1);
}
