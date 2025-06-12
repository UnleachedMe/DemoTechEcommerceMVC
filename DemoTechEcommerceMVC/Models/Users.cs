using Microsoft.AspNetCore.Identity;

namespace DemoTechEcommerceMVC.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }

    }
}
