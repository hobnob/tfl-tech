using System;
using System.Net.Http;

namespace tfl_tech.Models
{
    public class HttpClientWrapper : IHttpClient
    {
        public Uri BaseAddress {
            get {
                return httpClient.BaseAddress;
            }

            set {
                httpClient.BaseAddress = value;
            }
        }

        private HttpClient httpClient;

        public HttpClientWrapper(HttpClient initclient)
        {
            if (initclient == null) {
                throw new ArgumentNullException("HttpClient cannot be null");
            }

            httpClient = initclient;
        }

        public HttpResponseMessage Get(string uri)
        {
            return httpClient.GetAsync(uri).Result;
        }
    }
}
