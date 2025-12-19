using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.Models;
using MVC_Exercise.ServiceContract;

namespace MVC_Exercise.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInvoiceService _invoiceService;

        public HomeController(ILogger<HomeController> logger, IInvoiceService invoice)
        {
            _invoiceService = invoice;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<DashboardCount> counts = await _invoiceService.GetDashboardCount();
            ViewBag.Counts = counts;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
