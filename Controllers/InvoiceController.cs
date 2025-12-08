using Exercise_MVC.DBContext;
using Exercise_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Exercise.Models;
using MVC_Exercise.ServiceContract;
using System.Threading.Tasks;

namespace MVC_Exercise.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _InvoiceService;
        private readonly ApplicationDbContext _context;

        public InvoiceController(IInvoiceService InvoiceService, ApplicationDbContext context)
        {
            _InvoiceService = InvoiceService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<InvoiceDTO> Invoices = await _InvoiceService.GetAllInvoices();
            return View(Invoices);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            ViewBag.Products = _context.Products.Select(s => new { s.ProductId, s.ProductName, s.ProductRate});
            ViewBag.Partys = _context.Partys.Select(s => new { s.PartyId, s.PartyName});

            if (id == null)
            {
                return View(new Invoice());
            }
            else
            {
                Invoice Invoice = await _InvoiceService.GetInvoiceByIdAsync(id.Value);
                return View(Invoice);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(Invoice Invoice)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("Invoice", "Please complete all fields.");
                ViewBag.Products = _context.Products.Select(s => new { s.ProductId, s.ProductName, s.ProductRate });
                ViewBag.Partys = _context.Partys.Select(s => new { s.PartyId, s.PartyName });
                return View(Invoice);
            }

            if(Invoice.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must me greater than 0");
                ViewBag.Products = _context.Products.Select(s => new { s.ProductId, s.ProductName, s.ProductRate });
                ViewBag.Partys = _context.Partys.Select(s => new { s.PartyId, s.PartyName });
                return View(Invoice);
            }

            if (Invoice.InvoiceId == null) {
                bool result = await _InvoiceService.CreateInvoiceAsync(Invoice);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Products = _context.Products.Select(s => new { s.ProductId, s.ProductName, s.ProductRate });
                    ViewBag.Partys = _context.Partys.Select(s => new { s.PartyId, s.PartyName });
                    return View(Invoice);
                }
            }
            else
            {
                bool result = await _InvoiceService.UpdateInvoiceAsync(Invoice);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Products = _context.Products.Select(s => new { s.ProductId, s.ProductName, s.ProductRate });
                    ViewBag.Partys = _context.Partys.Select(s => new { s.PartyId, s.PartyName });
                    return View(Invoice);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _InvoiceService.DeleteInvoiceAsync(id);
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
