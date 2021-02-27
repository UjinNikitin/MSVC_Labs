using System.Collections.Generic;
using System.Threading.Tasks;
using LoggingSvc.Clients;
using LoggingSvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        public LogController(ILogger<LogController> logger, IStorageProviderClient storageProviderClient)
        {
            _logger = logger;
            _storageProviderClient = storageProviderClient;
        }


        [HttpGet]
        public Task<IEnumerable<string>> ListLogs() =>
            _storageProviderClient.Get();

        [HttpPost]
        public Task Log(Message msg)
        {
            _logger.LogInformation(msg.ToString());
            return _storageProviderClient.Insert(msg);
        }


        private readonly ILogger<LogController> _logger;
        private readonly IStorageProviderClient _storageProviderClient;
    }
}
