using System;
using System.Linq;
using System.Threading.Tasks;
using Facade.Clients;
using Facade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Facade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacadeController : ControllerBase
    {
        public FacadeController(
            ILogger<FacadeController> logger,
            ILoggingSvcClient loggingClient,
            IMessagesSvcClient messagesClient)
        {
            _logger = logger;
            _loggingClient = loggingClient;
            _messagesClient = messagesClient;
        }


        [HttpGet]
        public async Task<string> GetAll()
        {
            var logs = await _loggingClient.ListLogs();
            var messages = await _messagesClient.GetMessages();

            return $"{string.Join(",", logs.ToArray())}: {messages}";
        }

        [HttpPost]
        public async Task<StatusCodeResult> PostMsg(string msg)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Value = msg
            };

            _logger.LogInformation(msg);
            await _loggingClient.Log(message);

            return Ok();
        }


        private readonly ILogger<FacadeController> _logger;
        private readonly ILoggingSvcClient _loggingClient;
        private readonly IMessagesSvcClient _messagesClient;
    }
}
