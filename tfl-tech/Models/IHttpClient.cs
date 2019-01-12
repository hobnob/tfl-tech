using System;
using System.Net.Http;

namespace tfl_tech.Models
{
    public interface IHttpClient
    {
        Uri BaseAddress { get; set; }

        HttpResponseMessage Get(string uri);
    }
}
