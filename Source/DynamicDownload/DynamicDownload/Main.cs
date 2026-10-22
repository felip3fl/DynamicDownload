using Core;
using System.Reflection;

namespace DynamicDownload
{
    public class TestClass
    {
        [STAThread]
        static void Main(string[] args)
        {
            PrintBrandAndVerison();
            SetTerminalTitle("FL Dynamic Download");
            var downloadService = new DownloadService();

            var linkToDownload = getLinkFromParameters(args);

            if (linkToDownload == null)
                linkToDownload = getClipboardText();

            var resultMessage = downloadService.Download(linkToDownload);

            if (resultMessage == false)
                Console.ReadKey();
        }

        private static string getClipboardText()
        {
            string returnAudioStream = null;
            if (Clipboard.ContainsText())
            {
                returnAudioStream = Clipboard.GetText();
            }
            return returnAudioStream;
        }

        private static string getLinkFromParameters(string[] args)
        {
            return args.FirstOrDefault();
        }

        private static bool validLinkFromParameters(string[] args)
        {
            if (args.FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }

        private static void PrintBrandAndVerison()
        {
            var projectVersion = "v" + GetProjectVersion();
            var projectVersionLength = projectVersion.Length;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("____________________________");
            Console.WriteLine("\\  ________________________ \\");
            Console.WriteLine(" \\ \\        ____   __      \\ \\");
            Console.WriteLine("  \\ \\      / ___\\ /\\ \\      \\ \\");
            Console.WriteLine("   \\ \\    /\\ \\__/ \\ \\ \\      \\ \\");
            Console.WriteLine("    \\ \\   \\ \\  __\\ \\ \\ \\      \\ \\");
            Console.WriteLine("     \\ \\   \\ \\ \\_/  \\ \\ \\      \\ \\");
            Console.WriteLine("      \\ \\   \\ \\ \\    \\ \\ \\___   \\ \\");
            Console.WriteLine("       \\ \\   \\ \\_\\    \\ \\____\\   \\ \\");
            Console.WriteLine("        \\ \\   \\/_/     \\/____/    \\ \\");
            Console.WriteLine("         \\ \\_______________________\\ \\");


            Console.WriteLine($"          \\_______________{projectVersion}__\\");

            Console.WriteLine(@"");
            Console.ResetColor();
        }

        private static string GetProjectVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return version.ToString();
        }

        private static void SetTerminalTitle(string title)
        {
            Console.Title = title;
        }

    }


}
