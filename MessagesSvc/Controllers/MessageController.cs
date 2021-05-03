using System.Linq;
using System.Threading.Tasks;
using MessagesSvc.Clients;
using Microsoft.AspNetCore.Mvc;

namespace MessagesSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController(IStorageProviderClient storageProviderClient)
        {
            _storageProviderClient = storageProviderClient;
        }

        [HttpGet]
        public async Task<string> GetMessages() =>
            string.Join(", ", (await _storageProviderClient.Get()).ToArray());


        private IStorageProviderClient _storageProviderClient;
    }
}
