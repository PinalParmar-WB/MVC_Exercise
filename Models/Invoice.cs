using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise_MVC.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("Party")]
        public int PartyId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }


        public Party? Party { get; set; }
        public Product? Product { get; set; }
    }
}
