using GPTshka4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GPTshka4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IActionResult Index() => View();


        public IActionResult Privacy() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ChatFieldPartial()
        {

            return PartialView();
        }
        [HttpPost]
        public IActionResult ChatMessagePartial(string message)
        {

            return PartialView(new ChatMessageModel { Text = message });
        }
      
    }
}