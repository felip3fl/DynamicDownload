using System.Diagnostics;
using System.Text;

namespace Infra
{
    public class Terminal
    {
        readonly string executableAddress = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";
        readonly List<string> parameters = [
            "--check-formats",
            "--sponsorblock-remove sponsor,selfpromo",
            "--console-title",
            "-f $format",
            "'bestvideo[height<=1440]+(ba[format_note*=original]/ba)'", 
            "-o '%(channel)s - %(title)s.%(ext)s'",
            "-P 'temp:D:\\Downloads'",
            "--sub-langs 'enUS,en,en-US,pt,ptBR,pt-BR,br'",
            "--embed-subs",
            "--embed-thumbnail",
            "--add-metadata",
            ];

        public bool Download(string videoAddress)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();

            startInfo.FileName         = executableAddress;
            startInfo.WorkingDirectory = @"D:\Videos\Youtube Temp\";
            startInfo.Arguments        = "yt-dlp " + string.Join(" ", parameters) + " " + videoAddress;
            process.StartInfo          = startInfo;

            Console.WriteLine($"{string.Join(" ", parameters)} \n");
            process.Start();

            return true;
        }

        private void test(Process process)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.ResetColor();
        }

        public void ShowConsoleParameters()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{executableAddress} {string.Join(" ", parameters)} \n");
            Console.ResetColor();
        }

        public string CheckVideoDetails(string videoAddress)
        {
            var process   = new Process();
            var messages  = new StringBuilder();
            var startInfo = new ProcessStartInfo();

            if (videoAddress == "")
            {
                videoAddress = "https://www.youtube.com/watch?v=2Ylc0_g3AKU";    
            }

            startInfo.FileName               = executableAddress;
            startInfo.WorkingDirectory       = @"D:\Videos\Youtube Temp\";
            startInfo.Arguments              = "-F " + videoAddress;
            startInfo.RedirectStandardError  = true;
            //startInfo.RedirectStandardOutput = true;
            process.StartInfo                = startInfo;
            Process someProcess              = Process.Start(startInfo);

            //messages.Append(someProcess.StandardOutput.ReadToEnd());
            messages.Append(someProcess.StandardError.ReadToEnd());

            return messages.ToString();
        }
    }
}
