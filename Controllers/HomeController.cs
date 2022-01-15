using Microsoft.AspNetCore.Mvc;

namespace PlantCareFramework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
