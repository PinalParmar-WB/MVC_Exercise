using Exercise_MVC.DBContext;
using Exercise_MVC.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Exercise.ServiceContract;

namespace MVC_Exercise.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateProductAsync(Product model)
        {
            try
            {
                var product = await _context.Products.FindAsync(model.ProductId);

                if (product == null) {
                    return false;
                }

                product.ProductName = model.ProductName;
                product.ProductRate = model.ProductRate;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }
}
