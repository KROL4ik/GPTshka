using GPTshka4.Models.TestModels;
using Microsoft.AspNetCore.Mvc;

namespace GPTshka4.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View(new TestModel());
        }

        [HttpPost]
        public IActionResult Index(TestModel model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult GptTestPartial()
        {
            return PartialView();
        }
    }
}
