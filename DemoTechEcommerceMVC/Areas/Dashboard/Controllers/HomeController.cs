using Microsoft.AspNetCore.Mvc;

namespace DemoTechEcommerceMVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
