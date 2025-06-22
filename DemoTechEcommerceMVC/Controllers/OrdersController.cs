using DemoTechEcommerceMVC.Data;
using DemoTechEcommerceMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoTechEcommerceMVC.Controllers
{
    public class OrdersController : Controller
    {
        public readonly AppDbContext _context;
        public readonly UserManager<Users> _userManager;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var orders = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                orders = orders.Where(o =>
                    o.Id.ToString().Contains(searchTerm) ||
                    o.Status.Contains(searchTerm) ||
                    o.Amount.ToString().Contains(searchTerm));
            }

            var result = await orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return View("Index", result);
        }

    }
}
