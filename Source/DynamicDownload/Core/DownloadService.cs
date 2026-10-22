using Core.Model;
using File;
using Infra;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Text.RegularExpressions;

namespace Core
{
    public class DownloadService
    {
        private Terminal terminal = new();
        private List<Video> VideoList = new();

        public bool Download(string url)
        {
            var video = new Video();

            //LoadVideo();

            video.Url = RemoveAdditionalParameters(url);

            //if (CheckIfExistVideo(video.Url))
            //    return false;

            //terminal.ShowConsoleParameters();

            //var consoleMessage = terminal.CheckVideoDetails(url);

            //if (!string.IsNullOrEmpty(consoleMessage))
            //{
            //    var listOfMessages = ConvertMessageToList(consoleMessage);
            //    ShowErroConsole(listOfMessages);
            //    video.RealizeDate = CheckReturn(listOfMessages);
            //    //SalveVideo();
            //    return false;
            //}


            terminal.Download(url);
            

            return true;
        }

        public List<string> ConvertMessageToList(string messages)
        {
            var listOfMessages = messages.Split("\n").ToList();
            return listOfMessages;
        }

        private void SalveVideo()
        {
            JsonFile jsonFile = new JsonFile();
            jsonFile.Save(VideoList);
        }

        private bool CheckIfExistVideo(string url)
        {
            var search = VideoList.Where(f => f.Url == url).FirstOrDefault();
            if (String.IsNullOrEmpty(search.Url))
                return false;

            return true;
        }

        private void LoadVideo()
        {
            JsonFile jsonFile = new JsonFile();
            var fileContent = jsonFile.Load();
            var listVideo = fileContent.ConvertStringToVideo();
            VideoList = listVideo;
        }

        private void ShowErroConsole(List<string> listOfmessage)
        {
            foreach (var item in listOfmessage)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item);
                Console.ResetColor();
            }
        }

        private DateTime CheckReturn(List<string> listOfmessage)
        {
            //message = "This live event will begin in 11 days";
            //message = "ERROR: [youtube] 2Ylc0_g3AKU: Premieres in 52 minutes"

            var myRegex = new Regex(@"\d+ (minute|day|mounth|hour|second)");
            var resultList = listOfmessage.Where(f => myRegex.IsMatch(f)).ToList().FirstOrDefault();

            if (string.IsNullOrEmpty(resultList))
                return default;

            var futureDate = DateTime.Now;
            var daysRemaining = Regex.Match(resultList, @"\d+ (minute|day|mounth|hour|second)").Value;
            var values = daysRemaining.Split(" ");

            switch (values[1])
            {
                case "minute":
                    futureDate = futureDate.AddMinutes(int.Parse(values[0]));
                    break;
                case "hour":
                    futureDate = futureDate.AddHours(int.Parse(values[0]));
                    break;
                case "day":
                    futureDate = futureDate.AddDays(int.Parse(values[0]));
                    break;
                case "mounth":
                    futureDate = futureDate.AddMonths(int.Parse(values[0]));
                    break;
                default:
                    break;
            }

            //var dateRealize = dateNow.AddDays(1);

            return futureDate.AddHours(1);
        }

        private string RemoveAdditionalParameters(string url)
        {
            string urlWithNoParameters = "";
            int index = url.IndexOf("&");
            if (index >= 0)
                urlWithNoParameters = url.Substring(0, index);
            else
                return url;

            return urlWithNoParameters;
        }


    }

    public static class ObjectExtension
    {
        public static List<Video> ConvertStringToVideo(this string obj) {
            return JsonConvert.DeserializeObject<List<Video>>(obj);
        }
    }
}
