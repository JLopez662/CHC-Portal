using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;
using CPA.Models;
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

            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult GetDemograficoById(string id)
        {
            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            return Json(demografico);
        }
        [HttpPost]
        public IActionResult UpdateDemografico(Demografico model)
        {
            // Remove the Registro property from the model state validation
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);
                if (existingDemografico != null)
                {
                    // Copy the values from the incoming model to the existing entity
                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    existingDemografico.Dir = model.Dir;
                    existingDemografico.Tipo = model.Tipo;
                    existingDemografico.Patronal = model.Patronal;
                    existingDemografico.SSN = model.SSN;
                    existingDemografico.Incorporacion = model.Incorporacion;
                    existingDemografico.Operaciones = model.Operaciones;
                    existingDemografico.Industria = model.Industria;
                    existingDemografico.NAICS = model.NAICS;
                    existingDemografico.Descripcion = model.Descripcion;
                    existingDemografico.Contacto = model.Contacto;
                    existingDemografico.Telefono = model.Telefono;
                    existingDemografico.Celular = model.Celular;
                    existingDemografico.DirFisica = model.DirFisica;
                    existingDemografico.DirPostal = model.DirPostal;
                    existingDemografico.Email = model.Email;
                    existingDemografico.Email2 = model.Email2;
                    existingDemografico.CID = model.CID;
                    existingDemografico.MID = model.MID;

                    // Do not update the Registro property here as it's a navigation property

                    _customerService.UpdateDemografico(existingDemografico);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Demografico not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();

            foreach (var error in errors)
            {
                Console.WriteLine($"Model validation error: {error}");
            }

            return Json(new { success = false, message = "Model validation failed.", errors });
        }





    }
}
