using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace redis_sample.Controllers
{
    public class PublishController : Controller
    {
        private const string ChannelName = "ASP_CORE_CHAN";
        private readonly IRedisClient _redis;
        private readonly ILogger _logger;

        public PublishController(IRedisClient redisClient, ILogger<PublishController> logger)
        {
            _redis = redisClient;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string message)
        {
            _logger.LogInformation($"Message added: {message}");
            _redis.PublishMessage(ChannelName, message);

            return View();
        }
    }
}
