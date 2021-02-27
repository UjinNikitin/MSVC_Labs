using Microsoft.AspNetCore.Mvc;

namespace MessagesSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public string GetMessages() =>
            "Oh hello there (MessagesSvc)";
    }
}
