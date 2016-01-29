using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace TestIMDBUrls
{
    class Program
    {
        static void Main(string[] args)
        {
            int year = 2016;
            bool isSuccess = false;
            while (!isSuccess && year > 1951)
            {
                year--;
                //get auth token from http://api.cinemalytics.com/
                string authToken = "authToken";
                string api = string.Format(format: "http://api.cinemalytics.com/v1/movie/year/{0}/?auth_token={1}", arg0: year, arg1: authToken);
                string json;
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Proxy = null;
                    json = webClient.DownloadString(api);
                }
                List<Model> listmodel = JsonConvert.DeserializeObject<List<Model>>(json);
                foreach (Model model in listmodel)
                {
                    string moviename = Regex.Replace(model.OriginalTitle, pattern: @"[^\w\d]", replacement: "");
                    string url = string.Format(format: "http://wanderfull-microchip.weebly.com/snape-{0}.html", arg0: moviename);
                    try
                    {
                        var request = WebRequest.Create(url) as HttpWebRequest;
                        var response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            isSuccess = true;
                            Console.WriteLine(moviename);
                            Console.ReadLine();
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            if (!isSuccess)
            {
                Console.WriteLine(value: "nopes, no result found sorry.! :(");
            }

            Console.ReadLine();
        }
    }
}
