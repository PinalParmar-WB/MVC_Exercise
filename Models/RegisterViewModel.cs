using System.ComponentModel.DataAnnotations;

namespace MVC_Exercise.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must have length of 6.")]
        public string Password { get; set; }
    }
}