using System.ComponentModel.DataAnnotations;

namespace Exercise_MVC.Models
{
    public class Party
    {
        [Key]
        public int? PartyId { get; set; }

        [Required(ErrorMessage = "Party Name should not be empty")]
        public string PartyName { get; set; } = String.Empty;
    }
}
