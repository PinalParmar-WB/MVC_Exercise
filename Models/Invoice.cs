using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise_MVC.Models
{
    public class Invoice
    {
        [Key]
        public int? InvoiceId { get; set; }

        [ForeignKey("Party")]
        [Required(ErrorMessage = "Party Name must be required.")]
        public int PartyId { get; set; }

        [Required(ErrorMessage = "Product Name must be required.")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity must be required.")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Total Amount must be required.")]
        public double TotalAmount { get; set; }


        public Party? Party { get; set; }
        public Product? Product { get; set; }
    }
}
