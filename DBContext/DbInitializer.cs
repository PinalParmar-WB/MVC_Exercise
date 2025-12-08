using Microsoft.AspNetCore.Identity;
using MVC_Exercise.Models;

namespace MVC_Exercise.DBContext
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles to ensure exist
            var roles = new[] { "Admin", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Optional: Seed an admin user
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    DisplayName = "Site Admin"
                };
                var createAdmin = await userManager.CreateAsync(adminUser, "P@ssw0rd!"); // change password in prod
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Optional: Seed an employee user
            var empEmail = "employee@example.com";
            var empUser = await userManager.FindByEmailAsync(empEmail);
            if (empUser == null)
            {
                empUser = new ApplicationUser
                {
                    UserName = "employee",
                    Email = empEmail,
                    EmailConfirmed = true,
                    DisplayName = "Employee User"
                };
                var createEmp = await userManager.CreateAsync(empUser, "P@ssw0rd!"); // change password in prod
                if (createEmp.Succeeded)
                {
                    await userManager.AddToRoleAsync(empUser, "Employee");
                }
            }
        }
    }

}
