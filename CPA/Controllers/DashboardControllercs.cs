using Microsoft.AspNetCore.Mvc;

namespace CPA.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
