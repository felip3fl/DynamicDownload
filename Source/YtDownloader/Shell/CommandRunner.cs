using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace YtDownloader.Shell;

public sealed class CommandRunner
{
    private static readonly Regex SafeArgument = new(@"^[\w\-./:=+,]+$");

    public int Run(string program, IReadOnlyList<string> arguments)
    {
        using Process process = Start(program, arguments, false);
        process.WaitForExit();
        return process.ExitCode;
    }

    public CommandResult Capture(string program, IReadOnlyList<string> arguments)
    {
        using Process process = Start(program, arguments, true);
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return new CommandResult(process.ExitCode, output);
    }

    private static Process Start(string program, IReadOnlyList<string> arguments, bool captureOutput)
    {
        ProcessStartInfo info = new(program)
        {
            UseShellExecute = false,
            RedirectStandardOutput = captureOutput,
            StandardOutputEncoding = captureOutput ? Encoding.UTF8 : null
        };

        foreach (string argument in arguments)
            info.ArgumentList.Add(argument);

        Console.WriteLine($"> {program} {string.Join(' ', arguments.Select(Format))}");
        return Process.Start(info) ?? throw new InvalidOperationException($"Could not start {program}");
    }

    private static string Format(string argument) =>
        SafeArgument.IsMatch(argument) ? argument : $"\"{argument}\"";
}
