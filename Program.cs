using Exercise_MVC.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Exercise.DBContext;
using MVC_Exercise.Models;
using MVC_Exercise.ServiceContract;
using MVC_Exercise.Services;

namespace MVC_Exercise
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Identity: choose AddIdentity if you need roles and full customization
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password, lockout, user, sign in settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false; // set true to require email confirmation
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPartyService, PartyService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();

            // cookie settings (optional)
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // must be before Authorization
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // middleware...
            using (var scope = app.Services.CreateScope())
            {
                await DbInitializer.SeedAsync(scope.ServiceProvider);
            }

            app.Run();
        }
    }
}
