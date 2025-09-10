using DemoTechEcommerceMVC.Areas.Services;
using DemoTechEcommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;

namespace DemoTechEcommerceMVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdminController : Controller
    {
        private readonly StripeService _stripeService;
        private readonly AppDbContext _context;

        public AdminController(StripeService stripeService, AppDbContext context)
        {
            _stripeService = stripeService;
            _context = context;
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
