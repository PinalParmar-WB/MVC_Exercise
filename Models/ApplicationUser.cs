using Microsoft.AspNetCore.Identity;

namespace MVC_Exercise.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }   // extra property example
    }
}
