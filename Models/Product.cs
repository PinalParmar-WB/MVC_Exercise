using System.ComponentModel.DataAnnotations;

namespace Exercise_MVC.Models
{
    public class Product
    {
        [Key]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Product Name should not be empty")]
        public string ProductName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Product Rate should not be empty")]
        public double ProductRate { get; set; }

    }
}
