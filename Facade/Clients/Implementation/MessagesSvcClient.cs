using System.Net.Http;
using System.Threading.Tasks;

namespace Facade.Clients.Implementation
{
    public class MessagesSvcClient : IMessagesSvcClient
    {
        public MessagesSvcClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = "http://localhost:9090/";
        }


        public Task<string> GetMessages()
        {
            var uri = $"{_remoteServiceBaseUrl}api/Message";
            return _httpClient.GetStringAsync(uri);
        }


        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
    }
}
