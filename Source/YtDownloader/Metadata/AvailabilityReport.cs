using YtDownloader.Configuration;

namespace YtDownloader.Metadata;

public sealed class AvailabilityReport
{
    private readonly VideoMetadata metadata;
    private readonly AppSettings settings;

    public AvailabilityReport(VideoMetadata metadata, AppSettings settings)
    {
        this.metadata = metadata;
        this.settings = settings;
    }

    public void Print()
    {
        AudioCatalog audio = metadata.Audio();
        SubtitleCatalog subtitles = metadata.Subtitles();

        Console.WriteLine($"Video: {metadata.Channel()} - {metadata.Title()}");
        Console.WriteLine($"Original audio: {Describe(audio.Original()?.Id)}");
        Console.WriteLine($"Audio [{settings.DubbingLanguage}]: {Describe(audio.Dubbed(settings.DubbingLanguage)?.Id)}");

        foreach (string language in settings.SubtitleLanguages)
            Console.WriteLine($"Subtitle [{language}]: {Describe(subtitles.Find(language))}");

        if (settings.SubtitleLanguages.Length > 0 && !subtitles.AnyAvailable(settings.SubtitleLanguages))
            Console.WriteLine("Warning: no subtitles available for any configured language.");
    }

    private static string Describe(string? value) => value ?? "not available";
}
