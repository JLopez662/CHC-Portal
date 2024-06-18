﻿using Microsoft.AspNetCore.Mvc;
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

            var contributivos = _customerService.GetContributivos();

            var administrativos = _customerService.GetAdministrativos();
            
            var identificaciones = _customerService.GetIdentificaciones();

            var pagos = _customerService.GetPagos();

            var confidenciales = _customerService.GetConfidenciales();

            var viewModel = new DashboardViewModel
            {
                Demograficos = demograficos,
                Contributivos = contributivos,
                Administrativos = administrativos,
                Identificaciones = identificaciones,
                Pagos = pagos,
                Confidenciales = confidenciales
            };

            ViewBag.FirstName = TempData["FirstName"] as string;
            return View(viewModel);
        }
    }
}
