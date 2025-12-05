using Exercise_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.ServiceContract;
using System.Threading.Tasks;

namespace MVC_Exercise.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyService _PartyService;
        public PartyController(IPartyService PartyService)
        {
            _PartyService = PartyService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Party> Partys = await _PartyService.GetAllPartys();
            return View(Partys);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            if(id == null)
            {
                return View(new Party());
            }
            else
            {
                Party Party = await _PartyService.GetPartyByIdAsync(id.Value);
                return View(Party);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(Party Party)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("Party", "Please complete all fields.");
                return View(Party);
            }

            if (Party.PartyId == null) {
                bool result = await _PartyService.CreatePartyAsync(Party);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Party);
                }
            }
            else
            {
                bool result = await _PartyService.UpdatePartyAsync(Party);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Party);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _PartyService.DeletePartyAsync(id);
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
