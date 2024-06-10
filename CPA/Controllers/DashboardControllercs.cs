using BLL.Services;
using CPA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

            var demograficos = _customerService.GetDemograficos()
                .Select(d => new DemograficoViewModel
                {
                    ID = d.ID,
                    Nombre = d.Nombre,
                    NombreComercial = d.NombreComercial,
                    Dir = d.Dir,
                    Tipo = d.Tipo,
                    Patronal = d.Patronal,
                    SSN = d.SSN,
                    Incorporacion = d.Incorporacion,
                    Operaciones = d.Operaciones,
                    Industria = d.Industria,
                    NAICS = d.NAICS,
                    Descripcion = d.Descripcion,
                    Contacto = d.Contacto,
                    Telefono = d.Telefono,
                    Celular = d.Celular,
                    DirFisica = d.DirFisica,
                    DirPostal = d.DirPostal,
                    Email = d.Email,
                    Email2 = d.Email2,
                    CID = d.CID,
                    MID = d.MID
                }).ToList();

            var viewModel = new DashboardViewModel
            {
                Demograficos = demograficos
            };

            ViewBag.FirstName = TempData["FirstName"] as string;
            return View(viewModel);
        }
    }
}
