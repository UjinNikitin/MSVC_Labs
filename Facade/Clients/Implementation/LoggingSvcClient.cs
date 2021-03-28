using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Facade.Models;
using Newtonsoft.Json;

namespace Facade.Clients.Implementation
{
    public class LoggingSvcClient : ILoggingSvcClient
    {
        public LoggingSvcClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _random = new Random();
            _remoteServiceBaseUrls = new List<string>
            {
                "http://localhost:9091/",
                "http://localhost:9092/",
                "http://localhost:9093/"
            };
        }


        public async Task<IEnumerable<string>> ListLogs()
        {
            var uri = $"{GetServiceBaseUrl()}api/Log";
            var responseString = await _httpClient.GetStringAsync(uri);
            var messages = JsonConvert.DeserializeObject<IEnumerable<string>>(responseString);

            return messages;
        }

        public async Task Log(Message msg)
        {
            var uri = $"{GetServiceBaseUrl()}api/Log";
            await _httpClient.PostAsJsonAsync(uri, msg);
        }


        private string GetServiceBaseUrl() =>
            _remoteServiceBaseUrls[_random.Next(_remoteServiceBaseUrls.Count)];


        private readonly HttpClient _httpClient;
        private readonly List<string> _remoteServiceBaseUrls;
        private readonly Random _random;
    }
}
