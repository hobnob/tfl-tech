using System;
using tfl_tech.Models;
using tfl_tech.Views;

namespace tfl_tech.Controllers
{
    /// <summary>
    /// Controller for ccessing API routes
    /// </summary>
    public class ApiController
    {
        /// <summary>
        /// The API Client to use when using the API
        /// </summary>
        private ApiClient apiClient;

        /// <summary>
        /// Creates a new API Controller
        /// </summary>
        /// <param name="apiBase">The base of the API</param>
        /// <param name="appId">The App ID to use for this application</param>
        /// <param name="devKey">The developer Key to use</param>
        /// <param name="httpClient">The client to use when communicating with the API</param>
        public ApiController(Uri apiBase, string appId, string devKey, IHttpClient httpClient)
        {
            if (apiBase == null) {
                throw new ArgumentNullException("API base cannot be null");
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

            httpClient.BaseAddress = apiBase;
            apiClient = new ApiClient(httpClient, appId, devKey);
        }

        /// <summary>
        /// Gets a single road and returns the view to display
        /// </summary>
        /// <param name="roadName">The name of the road to display data for</param>
        /// <returns>A view with the data from the API populated</returns>
        public IView GetRoad(string roadName)
        {
            try {
                return new StringView(apiClient.GetRoadStatus(roadName).ToFormattedString());
            } catch (ArgumentException) {
                return new StringView(roadName + " is not a valid road", 1);
            } catch (Exception e) {
                return new StringView(e.Message, 1);
            }
        }
    }
}
