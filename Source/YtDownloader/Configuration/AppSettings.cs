namespace YtDownloader.Configuration;

public sealed record AppSettings(
    string TempFolder,
    string DestinationFolder,
    int MaxAttempts,
    string TitleFormat,
    string[] YtDlpArguments,
    string[] SubtitleLanguages,
    string DubbingLanguage,
    string HistoryFileName,
    string HistoryFolder,
    int DefaultQuality);
