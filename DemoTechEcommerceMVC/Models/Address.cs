using Microsoft.AspNetCore.Identity;

namespace DemoTechEcommerceMVC.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public IdentityUser User{ get; set; }

        public string AdressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
