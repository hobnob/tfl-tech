using System;
using System.Net.Http;

namespace tfl_tech.Models
{
    /// <summary>
    /// A basic wrapper for the HttpClient class making Unit tests a little easier
    /// </summary>
    public class HttpClientWrapper : IHttpClient
    {
        /// <summary>
        /// Gets and sets the base address of the underlying Http Client
        /// </summary>
        public Uri BaseAddress {
            get {
                return httpClient.BaseAddress;
            }

            set {
                httpClient.BaseAddress = value;
            }
        }

        /// <summary>
        /// The underlying Http Client
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Creats a new HttpClientWrapper
        /// </summary>
        /// <param name="initclient">The HTTP Client to use as the underlying object</param>
        public HttpClientWrapper(HttpClient initclient = null)
        {
            httpClient = initclient ?? new HttpClient();
        }

        /// <summary>
        /// Sends a GET request through the underlying HTTP Client and returns the result
        /// </summary>
        /// <param name="uri">The URI to send the request to</param>
        /// <returns>The response of the request</returns>
        public HttpResponseMessage Get(string uri)
        {
            return httpClient.GetAsync(uri).Result;
        }
    }
}
