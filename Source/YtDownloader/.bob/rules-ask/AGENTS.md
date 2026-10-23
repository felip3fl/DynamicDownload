# Project Documentation Context (Non-Obvious Only)

- There is **no test project** — all validation is done by running the application manually
- `Metadata/JsonFields.cs` is a static extension class for `JsonElement` — it is the sole abstraction over yt-dlp's JSON output
- `Urls/CommandLine.cs` parses only two things: the URL positional argument and `--quality <int>`; everything else is ignored
- `ClipboardReader` shells out to PowerShell `Get-Clipboard` — clipboard fallback is Windows-only by design
- The history file stores both the original URL and the cleaned URL; duplicate detection uses the **cleaned** URL (`Url` field, not `OriginalUrl`)
- `DownloadStatus.Scheduled` entries in history are retried automatically on the next successful run (via `DownloadWorkflow.RetryUnfinished`)
- yt-dlp format selection logic is in `Download/FormatExpression.cs` — the fallback chain matters for quality/audio selection
