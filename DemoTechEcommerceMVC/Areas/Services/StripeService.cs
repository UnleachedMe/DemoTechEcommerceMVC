using Stripe;
using System.Collections.Generic;
using System.Linq;

namespace DemoTechEcommerceMVC.Areas.Services
{
    public class StripeService
    {
        public List<Charge> GetCharges(int limit = 20)
        {
            var service = new ChargeService();
            var options = new ChargeListOptions
            {
                Limit = limit
            };
            var charges = service.List(options);
            return charges.Data.ToList();
        }

        public int GetTransactionCount()
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentListOptions
            {
                Limit = 10000 // Augmentez ce nombre si vous avez plus de transactions
            };

            var paymentIntents = service.List(options);
            return paymentIntents.Count();
        }
    }
}
