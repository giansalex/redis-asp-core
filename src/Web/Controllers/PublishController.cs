using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using redis_sample.Models;
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
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] string message)
        {
            _logger.LogInformation($"Message added: {message}");
            _redis.PublishMessage(ChannelName, message);

            return View();
        }
    }
}
