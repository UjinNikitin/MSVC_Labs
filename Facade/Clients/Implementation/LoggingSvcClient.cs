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
            _remoteServiceBaseUrl = "http://localhost:9091/";
        }


        public async Task<IEnumerable<string>> ListLogs()
        {
            var uri = $"{_remoteServiceBaseUrl}api/Log";
            var responseString = await _httpClient.GetStringAsync(uri);
            var messages = JsonConvert.DeserializeObject<IEnumerable<string>>(responseString);

            return messages;
        }

        public async Task Log(Message msg)
        {
            var uri = $"{_remoteServiceBaseUrl}api/Log";
            await _httpClient.PostAsJsonAsync(uri, msg);
        }


        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
    }
}
