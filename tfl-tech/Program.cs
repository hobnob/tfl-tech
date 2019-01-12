using System;
using System.Configuration;
using tfl_tech.Controllers;
using tfl_tech.Models;
using tfl_tech.Views;

namespace tfl_tech
{
    class Program
    {
        /// <summary>
        /// Entry point of the application
        /// </summary>
        /// <param name="args">The arguments passed to the app</param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            // Make sure taht the road has been passed
            if (args.Length < 1 || string.IsNullOrEmpty(args[0])) {
                Console.WriteLine("Please provide the road name as an argument");

                return 1;
            }

            // Create a new controller ready to deal with the request
            ApiController controller = new ApiController(
                new Uri(ConfigurationManager.AppSettings["tfl_api"]),
                ConfigurationManager.AppSettings["app_id"],
                ConfigurationManager.AppSettings["developer_key"],
                new HttpClientWrapper()
            );

            // et the view for this road
            IView view = controller.GetRoad(args[0]);

            // Output the view message and return the status
            Console.WriteLine(view.Output);
            return view.StatusCode;
        }
    }
}
