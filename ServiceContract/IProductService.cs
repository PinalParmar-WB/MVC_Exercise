using Exercise_MVC.Models;

namespace MVC_Exercise.ServiceContract
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<bool> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(Product model);
    }
}
