using Microsoft.AspNetCore.Mvc;
using Stripe;
using DemoTechEcommerceMVC.Areas.Services;

namespace DemoTechEcommerceMVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdminController : Controller
    {
        private readonly StripeService _stripeService;

        public AdminController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transactions()
        {
            var charges = _stripeService.GetCharges(50);
            return View(charges);
        }
    }
}
