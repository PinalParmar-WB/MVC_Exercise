using Exercise_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Exercise.Models
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int PartyId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }


        public string PartyName { get; set; } = string.Empty;
        public double ProductRate { get; set; }
        public string ProductName {  get; set; } = string.Empty;
    }
}
