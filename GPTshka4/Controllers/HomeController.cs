using apiTest.Models;
using GPTshka4.Context;
using GPTshka4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace GPTshka4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        private readonly ApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            Configuration = configuration;
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var listMessages = _applicationContext.Messages.Where(x => x.UserId == userId).ToList();

            return View(listMessages);
        }



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