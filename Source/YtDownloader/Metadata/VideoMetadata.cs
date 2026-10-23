using System.Text.Json;

namespace YtDownloader.Metadata;

public sealed class VideoMetadata
{
    private readonly JsonElement root;

    public VideoMetadata(JsonElement root)
    {
        this.root = root;
    }

    public string Id() => root.Text("id");

    public string Title() => root.Text("title");

    public string Channel() => root.Text("channel").Length > 0 ? root.Text("channel") : root.Text("uploader");

    public bool IsUpcoming() => root.Text("live_status") == "is_upcoming";

    public DateTimeOffset? ReleaseDate()
    {
        double timestamp = root.Number("release_timestamp");
        return timestamp > 0 ? DateTimeOffset.FromUnixTimeSeconds((long)timestamp).ToLocalTime() : null;
    }

    public AudioCatalog Audio() =>
        new(root.Items("formats").Where(IsAudioOnly).Select(ToAudioFormat));

    public SubtitleCatalog Subtitles() =>
        new(root.Keys("subtitles").Where(IsCaption), root.Keys("automatic_captions").Where(IsCaption));

    private static bool IsAudioOnly(JsonElement format) =>
        format.Text("vcodec") == "none" && format.Text("acodec") is not ("" or "none");

    private static AudioFormat ToAudioFormat(JsonElement format) =>
        new(
            format.Text("format_id"),
            format.Text("language"),
            format.Text("format_note"),
            Math.Max(format.Number("abr"), format.Number("tbr")));

    private static bool IsCaption(string key) => key != "live_chat";
}
