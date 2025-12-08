using Exercise_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.ServiceContract;
using System.Threading.Tasks;

namespace MVC_Exercise.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            if(id == null)
            {
                return View(new Product());
            }
            else
            {
                Product product = await _productService.GetProductByIdAsync(id.Value);
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("Product", "Please complete all fields.");
                return View(product);
            }

            if (product.ProductId == null) {
                bool result = await _productService.CreateProductAsync(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            else
            {
                bool result = await _productService.UpdateProductAsync(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _productService.DeleteProductAsync(id);
            if (res)
            {
                ViewBag.SuccessMsg = "Deleted successfully!!!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMsg = "Deletion failed!!!";
                return RedirectToAction("Index");
            }
        }
    }
}
