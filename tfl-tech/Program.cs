using System;
using System.Configuration;
using tfl_tech.Controllers;
using tfl_tech.Models;
using tfl_tech.Views;

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

            ApiController controller = new ApiController(
                new Uri(ConfigurationManager.AppSettings["tfl_api"]),
                ConfigurationManager.AppSettings["app_id"],
                ConfigurationManager.AppSettings["developer_key"],
                new HttpClientWrapper()
            );

            IView view = controller.GetRoad(args[0]);

            Console.WriteLine(view.Output);
            return view.StatusCode;
        }
    }
}
