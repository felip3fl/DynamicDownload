# AGENTS.md

This file provides guidance to agents when working with code in this repository.

## Stack
- **C# / .NET 8**, console application (`OutputType=Exe`), no test project exists
- **ImplicitUsings** and **Nullable** are enabled project-wide
- Runtime dependency: `yt-dlp` must be on PATH; clipboard reading uses PowerShell `Get-Clipboard`

## Build & Run Commands
```
dotnet build                                      # build
dotnet run -- <url> [--quality <height>]         # run with URL argument
dotnet run                                        # run with URL read from clipboard
dotnet publish -c Release                         # publish
```
There are no tests in this project.

## Architecture Flow
```
Program.cs → Application.Run()
  → UrlResolver: arg URL or PowerShell clipboard fallback
  → DownloadWorkflow.Run()
      → HistoryStore.IsDone() → skip duplicate
      → VideoDownloader.Download() (retries up to MaxAttempts)
          → MetadataProbe → yt-dlp -J (JSON metadata)
          → AvailabilityReport.Print()
          → yt-dlp with DownloadArguments → temp folder per video ID
          → FileMover: moves files from temp folder → DestinationFolder
      → HistoryStore.Record()
      → Retry all Unfinished history entries
```

## Configuration (`appsettings.json`)
- Loaded from `AppContext.BaseDirectory` (next to the executable), **not** the working directory
- `TempFolder`, `DestinationFolder`, `HistoryFolder` support `%ENVVAR%` expansion via `Environment.ExpandEnvironmentVariables`
- `TitleFormat` is a yt-dlp output template string (e.g. `%(channel)s - %(title)s.%(ext)s`)
- `YtDlpArguments` are prepended to every yt-dlp download invocation
- `DefaultQuality` is a video height in pixels (e.g. `1440`)

## Code Style Conventions
- All classes are `sealed` — do not create unsealed concrete classes
- Data-only types use `sealed record`; behavioral types use `sealed class`
- Fields are always `private readonly`, injected via constructor (manual DI, no container)
- No static mutable state; static members are pure helpers or constants only
- `JsonElement` extension methods live in `Metadata/JsonFields.cs` (`Text()`, `Number()`, `Items()`, `Keys()`) — use these instead of raw `TryGetProperty`
- History JSON uses camelCase + string enum serialization (configured in `HistoryStore` options)
- `VideoUrl.ToString()` returns the cleaned URL (only `?v=` preserved); `VideoUrl.Original()` returns the raw input
- `CommandRunner` uses `ArgumentList` (never string concatenation) to avoid shell injection
- Error handling: throw exceptions to bubble up to `Application.Run()` which catches and returns exit code 1

## Key Domain Rules
- A video is considered "done" only if `HistoryStore.IsDone()` finds `Status == DownloadStatus.Done` for the cleaned URL
- `DownloadStatus.Scheduled` means the video is a future premiere — it is saved to history but not downloaded
- After a successful download, `DownloadWorkflow` automatically retries all `Unfinished` (non-Done) history entries
- Audio selection in `FormatExpression`: prefers explicit original + dubbed audio track IDs; falls back to `+ba`
- Subtitle preference order: manual captions before automatic captions (`SubtitleCatalog.Find`)
- yt-dlp downloads go into an isolated temp subfolder `yt-dlp-{videoId}` inside `TempFolder`; this folder is deleted after files are moved
