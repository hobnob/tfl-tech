using System;
using System.Net.Http;

namespace tfl_tech.Models
{
    /// <summary>
    /// An interface for dealing with a HTTP Client
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// The base address
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Sends a GET request to the URI and returns the result
        /// </summary>
        /// <param name="uri">The URI to send the request to</param>
        /// <returns>The response of the request</returns>
        HttpResponseMessage Get(string uri);
    }
}
