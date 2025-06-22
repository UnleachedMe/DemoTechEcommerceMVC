using DemoTechEcommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace DemoTechEcommerceMVC.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public PaymentsController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Payment()
        {
            return View();

        }

        
        public IActionResult CreateCheckoutSession(int orderId)
        {
            var order = _context.Orders
               .Include(o => o.OrderProducts)
                   .ThenInclude(op => op.Product)
               .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                return BadRequest("Commande introuvable.");

            var domain = "https://localhost:7257"; // à remplacer en production

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var op in order.OrderProducts)
            {

                var productImageUrl = string.IsNullOrEmpty(op.Product.Image)
    ? $"{domain}/img/products/default-product.jpg"
    : $"{domain}{op.Product.Image}";

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(op.Price * 100),
                        Currency = "mad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = op.Product.Name,
                            Description = op.Product.Description ?? "Aucun descriptif",
                            Images = new List<string> { productImageUrl }
                        }
                    },
                    Quantity = op.Qty
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + $"/checkout/thankyou?orderId={order.Id}",
                CancelUrl = domain + "/payments/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url); // Redirige vers la page Stripe Checkout
        }

        public IActionResult Success()
        {
            return RedirectToAction("Thankyou", "Checkout");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index", "Cart");
        }
    }
}
