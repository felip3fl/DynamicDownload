# Project Coding Rules (Non-Obvious Only)

- All classes must be `sealed` — no unsealed concrete classes anywhere in the project
- Data-only types use `sealed record`; behavioral types use `sealed class` (no exceptions observed)
- Constructor injection only — no DI container, no service locator, no static factories
- Use `JsonFields` extension methods (`Text()`, `Number()`, `Items()`, `Keys()` in `Metadata/JsonFields.cs`) for all `JsonElement` access — never call `TryGetProperty` directly
- `CommandRunner.Run()` uses `ProcessStartInfo.ArgumentList` — never build shell command strings with concatenation
- `appsettings.json` is read from `AppContext.BaseDirectory`, not `Directory.GetCurrentDirectory()` — keep this path logic in `SettingsLoader`
- `VideoUrl.ToString()` returns the cleaned URL (strips all query params except `?v=`); use `.Original()` only when you need the raw user input
- Static members must be pure (no side effects, no mutation) — all mutable state lives in instance fields
