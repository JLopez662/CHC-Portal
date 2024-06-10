using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using CPA.Models;

namespace CPA.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICustomerService _customerService;

        public DashboardController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            TempData["Success"] = "You have successfully logged in!";
            var demograficos = _customerService.GetDemograficos();

            var viewModel = new DashboardViewModel
            {
                Demograficos = demograficos
            };

            ViewBag.FirstName = TempData["FirstName"] as string;
            return View(viewModel);
        }
    }
}
