using System.Net.Http;
using System;
using System.Net;
using Newtonsoft.Json;

namespace tfl_tech.Models
{
    public class ApiClient
    {
        private HttpClient httpClient;
        private string appId;
        private string developerKey;

        public ApiClient(HttpClient initHttpClient, string appId, string developerKey)
        {
            if (initHttpClient == null) {
                throw new ArgumentNullException("HttpClient cannot be null");
            }

            if (initHttpClient.BaseAddress == null || string.IsNullOrEmpty(initHttpClient.BaseAddress.ToString())) {
                throw new ArgumentException("HttpClient is not configured with a base address. Please set the base address to the TFL API root");
            }

            httpClient = initHttpClient;
            this.appId = appId;
            this.developerKey = developerKey;
        }

        public RoadStatus GetRoadStatus(string roadName)
        {
            UriBuilder uri = new UriBuilder("/Road/" + roadName);
            uri.Query = "app_id=" + appId + "&app_key=" + developerKey;

            HttpResponseMessage response = httpClient.GetAsync(uri.Uri).Result;

            switch (response.StatusCode) {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<RoadStatus>(response.Content.ToString());
                case HttpStatusCode.NotFound:
                    throw new ArgumentException(roadName + " is not a valid road");
                default:
                    throw new Exception("An unknown status was returned by the API (" + ((int) response.StatusCode) + ")");
            }
        }
    }
}
