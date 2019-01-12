using System;
using tfl_tech.Models;
using tfl_tech.Views;

namespace tfl_tech.Controllers
{
    class ApiController
    {
        private ApiClient apiClient;

        public ApiController(Uri apiRoute, string appId, string devKey, IHttpClient httpClient)
        {
            if (apiRoute == null) {
                throw new ArgumentNullException("API Route cannot be null");
            }

            if (httpClient == null) {
                throw new ArgumentNullException("HTTP Client cannot be null");
            }

            if (string.IsNullOrEmpty(appId)) {
                throw new ArgumentNullException("App ID  must be supplied");
            }
            
            if (string.IsNullOrEmpty(devKey)) {
                throw new ArgumentNullException("Developer Key  must be supplied");
            }

            httpClient.BaseAddress = apiRoute;
            apiClient = new ApiClient(httpClient, appId, devKey);
        }

        public IView GetRoad(string roadName)
        {
            try {
                return new ConsoleView(apiClient.GetRoadStatus(roadName).ToFormattedString());
            } catch (ArgumentException) {
                return new ConsoleView(roadName + " is not a valid road", 1);
            } catch (Exception e) {
                return new ConsoleView(e.Message, 1);
            }
        }
    }
}
