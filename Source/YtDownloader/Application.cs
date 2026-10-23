using System.Text;
using YtDownloader.Configuration;
using YtDownloader.Download;
using YtDownloader.History;
using YtDownloader.Shell;
using YtDownloader.Urls;
using YtDownloader.Workflow;

namespace YtDownloader;

public sealed class Application
{
    private readonly string[] arguments;

    public Application(string[] arguments)
    {
        this.arguments = arguments;
    }

    public int Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        try
        {
            return Execute();
        }
        catch (Exception exception)
        {
            return Fail(exception.Message);
        }
    }

    private int Execute()
    {
        AppSettings settings = new SettingsLoader(Path.Combine(AppContext.BaseDirectory, "appsettings.json")).Load();
        CommandLine commandLine = new(arguments);
        CommandRunner runner = new();
        VideoUrl url = new UrlResolver(commandLine, new ClipboardReader(runner)).Resolve();
        DownloadWorkflow workflow = new(new VideoDownloader(settings, runner), new HistoryStore(settings), settings);
        DownloadStatus status = workflow.Run(url, commandLine.Quality(settings.DefaultQuality));
        return status == DownloadStatus.Failed ? Fail("Download failed after all attempts") : 0;
    }

    private static int Fail(string message)
    {
        Console.WriteLine($"ERROR: {message}");
        Console.Write("Press Enter to exit...");
        Console.ReadLine();
        return 1;
    }
}
