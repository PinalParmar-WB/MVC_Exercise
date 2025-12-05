using Exercise_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise_MVC.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Party> Partys { get; set; }
        public  DbSet<Invoice> Invoices { get; set; }
    }
}
