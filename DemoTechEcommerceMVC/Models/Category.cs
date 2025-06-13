using System.ComponentModel.DataAnnotations;

namespace DemoTechEcommerceMVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        }
}
