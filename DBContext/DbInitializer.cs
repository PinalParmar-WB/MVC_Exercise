using Microsoft.AspNetCore.Identity;
using MVC_Exercise.Models;

namespace MVC_Exercise.DBContext
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Admin user
            var adminEmail = "admin@example.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true, DisplayName = "Administrator" };
                var result = await userManager.CreateAsync(admin, "@Admin1"); // strong password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                // handle errors if any
            }
        }
    }

}
