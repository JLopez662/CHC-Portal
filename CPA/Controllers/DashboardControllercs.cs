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
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);
                if (existingDemografico != null)
                {
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

                    _customerService.UpdateDemografico(existingDemografico);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Demografico not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

        [HttpGet]
        public IActionResult GetContributivoById(string id)
        {
            var contributivo = _customerService.GetContributivos().FirstOrDefault(c => c.ID == id);
            if (contributivo == null)
            {
                return NotFound();
            }

            // Convert the date to a string in the proper format (yyyy-MM-dd)
            var contributivoViewModel = new
            {
                contributivo.ID,
                contributivo.Nombre,
                contributivo.NombreComercial,
                contributivo.Estatal,
                contributivo.Poliza,
                contributivo.RegComerciante,
                Vencimiento = contributivo.Vencimiento.ToString("yyyy-MM-dd"),
                contributivo.Choferil,
                contributivo.DeptEstado,
                contributivo.CID,
                contributivo.MID
            };

            return Json(contributivoViewModel);
        }

        [HttpPost]
        public IActionResult UpdateContributivo(Contributivo model)
        {
            // Remove validation for the Registro field if it's optional
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingContributivo = _customerService.GetContributivos().FirstOrDefault(c => c.ID == model.ID);
                if (existingContributivo != null)
                {
                    existingContributivo.Nombre = model.Nombre;
                    existingContributivo.NombreComercial = model.NombreComercial;
                    existingContributivo.Estatal = model.Estatal;
                    existingContributivo.Poliza = model.Poliza;
                    existingContributivo.RegComerciante = model.RegComerciante;
                    existingContributivo.Vencimiento = model.Vencimiento;
                    existingContributivo.Choferil = model.Choferil;
                    existingContributivo.DeptEstado = model.DeptEstado;
                    existingContributivo.CID = model.CID;
                    existingContributivo.MID = model.MID;

                    _customerService.UpdateContributivo(existingContributivo);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Contributivo not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

        [HttpGet]
        public IActionResult GetAdministrativoById(string id)
        {
            var administrativo = _customerService.GetAdministrativos().FirstOrDefault(a => a.ID == id);
            if (administrativo == null)
            {
                return NotFound();
            }

            // Convert the date fields to a string in the proper format (yyyy-MM-ddTHH:mm)
            var administrativoViewModel = new
            {
                administrativo.ID,
                administrativo.Nombre,
                administrativo.NombreComercial,
                administrativo.Contrato,
                administrativo.Facturacion,
                administrativo.FacturacionBase,
                administrativo.IVU,
                administrativo.Staff,
                administrativo.StaffDate,
                administrativo.CID,
                administrativo.MID
            };

            return Json(administrativoViewModel);
        }

        [HttpPost]
        public IActionResult UpdateAdministrativo(Administrativo model)
        {
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingAdministrativo = _customerService.GetAdministrativos().FirstOrDefault(a => a.ID == model.ID);
                if (existingAdministrativo != null)
                {
                    existingAdministrativo.Nombre = model.Nombre;
                    existingAdministrativo.NombreComercial = model.NombreComercial;
                    existingAdministrativo.Contrato = model.Contrato;
                    existingAdministrativo.Facturacion = model.Facturacion;
                    existingAdministrativo.FacturacionBase = model.FacturacionBase;
                    existingAdministrativo.IVU = model.IVU;
                    existingAdministrativo.Staff = model.Staff;
                    existingAdministrativo.StaffDate = model.StaffDate;
                    existingAdministrativo.CID = model.CID;
                    existingAdministrativo.MID = model.MID;

                    _customerService.UpdateAdministrativo(existingAdministrativo);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Administrativo not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

        [HttpGet]
        public IActionResult GetIdentificacionById(string id)
        {
            var identificacion = _customerService.GetIdentificaciones().FirstOrDefault(i => i.ID == id);
            if (identificacion == null)
            {
                return NotFound();
            }

            return Json(identificacion);
        }

        [HttpPost]
        public IActionResult UpdateIdentificacion(Identificacion model)
        {
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingIdentificacion = _customerService.GetIdentificaciones().FirstOrDefault(i => i.ID == model.ID);
                if (existingIdentificacion != null)
                {
                    existingIdentificacion.Nombre = model.Nombre;
                    existingIdentificacion.NombreComercial = model.NombreComercial;
                    existingIdentificacion.Accionista = model.Accionista;
                    existingIdentificacion.SSNA = model.SSNA;
                    existingIdentificacion.Cargo = model.Cargo;
                    existingIdentificacion.LicConducir = model.LicConducir;
                    existingIdentificacion.Nacimiento = model.Nacimiento;
                    existingIdentificacion.CID = model.CID;
                    existingIdentificacion.MID = model.MID;

                    _customerService.UpdateIdentificacion(existingIdentificacion);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Identificacion not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

    }
}
