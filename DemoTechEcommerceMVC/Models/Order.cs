﻿using Microsoft.AspNetCore.Identity;

namespace DemoTechEcommerceMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public int AddressId { get; set; }

        public Address Address  { get; set; }

        public double Amount { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }


    }
}
