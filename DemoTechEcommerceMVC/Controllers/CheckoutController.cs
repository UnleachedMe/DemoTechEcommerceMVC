using DemoTechEcommerceMVC.Data;
using DemoTechEcommerceMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoTechEcommerceMVC.Controllers
{
    public class CheckoutController : Controller
    {
        public readonly AppDbContext _context;
        public readonly UserManager<Users> _userManager;

        public CheckoutController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            var addresses = await _context.Adresses
                .Include(x => x.User)
                .Where(x => x.UserId == currentuser.Id)
                .ToListAsync();

            ViewBag.Adresses = addresses;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Address address)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);


                address.UserId = currentuser.Id;

                _context.Adresses.Add(address);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(address);
        }

        public async Task<IActionResult> Confirm(int addressId)
        {

            var address = await _context.Adresses.Where(a => a.Id == addressId).FirstOrDefaultAsync();
            if (address == null)
            {
                return BadRequest();
            }

            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            double orderCost = 0;

            var carts = await _context.Carts
                .Include(x => x.Product)
                .Where(c => c.UserId == currentuser.Id).ToListAsync();

            foreach(var cart in carts)
            {
                orderCost += (cart.Product.Price * cart.Qty);
            }

            var order = new Order
            {
                AddressId = address.Id,
                Status = "Order placed",
                CreatedAt = DateTime.Now,
                UserId = currentuser.Id,
                Amount = orderCost,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach(var cart in carts)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = cart.ProductId,
                    OrderId = order.Id,
                    Price = cart.Product.Price,
                    Qty = cart.Qty,
                        
                };
                

                _context.OrderProducts.Add(orderProduct);
            }

            await _context.SaveChangesAsync();
            _context.RemoveRange(carts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Thankyou");
        }

        public async Task<IActionResult> ThankyouAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var latestOrder = await _context.Orders
                .Include(o => o.Address)
                .Where(o => o.UserId == currentUser.Id)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            return View(latestOrder);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                // Récupérer l'utilisateur courant
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser == null)
                {
                    return Unauthorized();
                }

                // Trouver l'adresse à supprimer
                var address = await _context.Adresses
                    .FirstOrDefaultAsync(a => a.Id == id && a.UserId == currentUser.Id);

                if (address == null)
                {
                    return NotFound();
                }

                // Supprimer l'adresse
                _context.Adresses.Remove(address);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
