using System.Net.Http;
using System;
using System.Net;
using Newtonsoft.Json;

namespace tfl_tech.Models
{
    /// <summary>
    /// Represents a client that interacts with the API
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// The HTTP Client to use when communicating with the API
        /// </summary>
        private IHttpClient httpClient;

        /// <summary>
        /// The App ID to use for this application
        /// </summary>
        private string appId;

        /// <summary>
        /// The developer key to use
        /// </summary>
        private string developerKey;

        /// <summary>
        /// Creates a new API Client
        /// </summary>
        /// <param name="initHttpClient">The HTTP client to use when comminicating with the API</param>
        /// <param name="appId">The App ID to use</param>
        /// <param name="developerKey">The developer key to use</param>
        public ApiClient(IHttpClient initHttpClient, string appId, string developerKey)
        {
            if (initHttpClient == null) {
                throw new ArgumentNullException("HttpClient cannot be null");
            }

            if (initHttpClient.BaseAddress == null || string.IsNullOrEmpty(initHttpClient.BaseAddress.ToString())) {
                throw new ArgumentException("HttpClient is not configured with a base address. Please set the base address to the TFL API root");
            }

            if (string.IsNullOrEmpty(appId)) {
                throw new ArgumentNullException("App ID must not be null or empty");
            }

            if (string.IsNullOrEmpty(developerKey)) {
                throw new ArgumentNullException("Developer Key must not be null or empty");
            }

            httpClient = initHttpClient;
            this.appId = appId;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// Gets the road status of a specified road
        /// </summary>
        /// <param name="roadName">The road to look up</param>
        /// <returns>A road status struct with details of that specific road</returns>
        /// <exception cref="System.ArgumentException">Thrown if the road is not valid</exception>
        /// <exception cref="System.Exception">Thrown if the API sends an unknown status</exception>
        public RoadStatus GetRoadStatus(string roadName)
        {
            // Build the URI ready to send to the API
            Uri uri = new Uri(
                httpClient.BaseAddress,
                Uri.EscapeUriString("/Road/" + roadName) + "?app_id=" + Uri.EscapeDataString(appId) + "&app_key=" + Uri.EscapeDataString(developerKey)
            );

            // Call the API using the new URI
            HttpResponseMessage response = httpClient.Get(uri.ToString());
            
            switch (response.StatusCode) {
                case HttpStatusCode.OK:
                    // On success deserialise the expected road status array and return the first result (as we're only after 1 road)
                    return JsonConvert.DeserializeObject<RoadStatus[]>(response.Content.ReadAsStringAsync().Result)[0];
                case HttpStatusCode.NotFound:
                    // Throw an exception if the road isn't found
                    throw new ArgumentException(roadName + " is not a valid road");
                default:
                    // Any other status should result in an exception too
                    throw new Exception("An unknown status was returned by the API (" + ((int) response.StatusCode) + ")");
            }
        }
    }
}
