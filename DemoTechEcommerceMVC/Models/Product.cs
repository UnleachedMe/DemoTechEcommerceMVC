using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoTechEcommerceMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string Image { get; set; }

        //public string Category { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Category")]
        public Category CategoryNavigation { get; set; }
    }
}
