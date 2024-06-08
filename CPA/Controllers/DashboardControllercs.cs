using Microsoft.AspNetCore.Mvc;

namespace CPA.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.FirstName = TempData["FirstName"] as string;
            return View();
        }

    }
}
