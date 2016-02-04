using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace TestIMDBUrls
{
    class Program
    {
        static void Main(string[] args)
        {
            int year = Convert.ToInt32(ConfigurationManager.AppSettings["MovieYearStartingLatest"]);
            int endingYear = Convert.ToInt32(ConfigurationManager.AppSettings["MovieEndingYearOldest"]);
            bool isSuccess = false;
            while (!isSuccess && year > 1951)
            {
                //get auth token from http://api.cinemalytics.com/
                string authToken = ConfigurationManager.AppSettings["MovieAuthToken"];
                string api = string.Format(format: ConfigurationManager.AppSettings["MovieAPIUrl"], arg0: year, arg1: authToken);
                string json;
                using (var webClient = new WebClient())
                {
                    webClient.Proxy = null;
                    json = webClient.DownloadString(api);
                }
                List<Model> listmodel = JsonConvert.DeserializeObject<List<Model>>(json);
                foreach (Model model in listmodel)
                {
                    string moviename = Regex.Replace(model.OriginalTitle, pattern: @"[^\w\d]", replacement: "");
                    string treasureHuntUrl = ConfigurationManager.AppSettings["TreasureHuntUrl"];
                    string url = string.Format(format: treasureHuntUrl, arg0: "hagrid", arg1: moviename);
                    try
                    {
                        var request = WebRequest.Create(url) as HttpWebRequest;
                        var response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            isSuccess = true;
                            Console.WriteLine(moviename);
                            Console.WriteLine("Movie url:" + url);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                year--;
            }
            if (!isSuccess)
            {
                Console.WriteLine(value: "nopes, no result found sorry.! :(");
            }

            Console.ReadLine();
        }
    }
}
