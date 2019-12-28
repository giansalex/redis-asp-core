using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using redis_sample.Models;

namespace redis_sample.Controllers
{
    public class HomeController : Controller
    {
        private const string CacheKey = "ASP_APP1";
        private readonly IDistributedCache _distributedCache;

        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index(bool write = false)
        {
            if (write) {
                await _distributedCache.SetStringAsync(CacheKey, DateTime.UtcNow.ToString());

                ViewData["Message"] = "Write Cache";
                return View();
            }

		    var value = await _distributedCache.GetStringAsync(CacheKey);
            ViewData["Message"] = "Value Read: " + value;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
