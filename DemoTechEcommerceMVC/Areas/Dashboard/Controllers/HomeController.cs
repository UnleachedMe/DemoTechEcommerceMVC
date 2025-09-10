using DemoTechEcommerceMVC.Areas.ModelViews;
using DemoTechEcommerceMVC.Areas.Services;
using DemoTechEcommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoTechEcommerceMVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly StripeService _stripeService;

        public HomeController(AppDbContext context, StripeService stripeService)
        {
            _context = context;
            _stripeService = stripeService;
        }
        public IActionResult Index()
        {
            var currentDate = DateTime.Now;
            var orders = _context.Orders
                .Where(o => o.CreatedAt.Year == currentDate.Year &&
                           o.CreatedAt.Month == currentDate.Month)
                .ToList();

            var orders2 = _context.Orders
                .Where(o => o.CreatedAt.Year == currentDate.Year)
                .ToList();

            double sumOrders = 0;
            foreach (var order in orders)
            {
                // Vérification null + valeur par défaut
                sumOrders += order.Amount;
            }

            double sumOrders2 = 0;
            foreach (var order2 in orders2)
            {
                // Vérification null + valeur par défaut
                sumOrders2 += order2.Amount;
            }

            // Passage des données à la vue
            ViewBag.Orders = orders; // Liste complète si besoin
            ViewBag.TotalMoisEnCours = sumOrders;

            var vm = new DashboardVM
            {
                MonthlyEarnings = sumOrders,
                AnnualEarnings = sumOrders2,
                TotalOrders = _context.Orders.Count(),
                TotalTransactions = _stripeService.GetTransactionCount()
            };

            


            return View(vm);
        }
    }
}
