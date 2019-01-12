using System;
using System.Configuration;
using tfl_tech.Models;

namespace tfl_tech
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1 || string.IsNullOrEmpty(args[0])) {
                Console.WriteLine("Please provide the road name as an argument");

                return 1;
            }

            HttpClientWrapper httpClient = new HttpClientWrapper();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["tfl_api"]);

            ApiClient client = new ApiClient(
                httpClient,
                ConfigurationManager.AppSettings["app_id"],
                ConfigurationManager.AppSettings["developer_key"]
            );

            try {
                Console.WriteLine(client.GetRoadStatus(args[0]).ToFormattedString());
            } catch (ArgumentException) {
                Console.WriteLine(args[0] + " is not a valid road");

                return 1;
            } catch (Exception e) {
                Console.WriteLine(e.Message);

                return 1;
            }

            return 0;
        }
    }
}
