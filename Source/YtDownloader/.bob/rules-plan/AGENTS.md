# Project Architecture Rules (Non-Obvious Only)

- The app is a single-pass CLI tool with no async code — all I/O is synchronous
- `VideoDownloader` retries the full pipeline (metadata probe + download) up to `MaxAttempts` times per video
- After any successful download, `DownloadWorkflow` automatically processes **all** unfinished history entries — new features must not break this side-effect
- yt-dlp is invoked twice per video: once with `-J` for metadata (captured), once for the actual download (streamed to stdout)
- Files are downloaded into an isolated temp subfolder `yt-dlp-{videoId}` inside `TempFolder`; `FileMover` moves them to `DestinationFolder` and deletes the subfolder — any new output files must go through this same temp-then-move pattern
- `AppSettings` is an immutable `record` with environment variable expansion applied at load time — add new config properties as record members in `Configuration/AppSettings.cs` and map them in `appsettings.json`
- No dependency injection framework — wiring happens entirely in `Application.Execute()`; keep new dependencies explicit there
