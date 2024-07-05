using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;
using CPA.Models;
using System.Linq;
using BLL.Interfaces;

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
            var contributivos = _customerService.GetContributivos().ToList();
            foreach (var contributivo in contributivos)
            {
                var demografico = demograficos.FirstOrDefault(d => d.ID == contributivo.ID);
                if (demografico != null)
                {
                    contributivo.Nombre = demografico.Nombre;
                    contributivo.NombreComercial = demografico.NombreComercial;
                }
            }

            var administrativos = _customerService.GetAdministrativos();
            foreach (var administrativo in administrativos)
            {
                var demografico = demograficos.FirstOrDefault(d => d.ID == administrativo.ID);
                if (demografico != null)
                {
                    administrativo.Nombre = demografico.Nombre;
                    administrativo.NombreComercial = demografico.NombreComercial;
                }
            }

            var identificaciones = _customerService.GetIdentificaciones();
            foreach (var identificacion in identificaciones)
            {
                var demografico = demograficos.FirstOrDefault(d => d.ID == identificacion.ID);
                if (demografico != null)
                {
                    identificacion.Nombre = demografico.Nombre;
                    identificacion.NombreComercial = demografico.NombreComercial;
                }
            }

            var pagos = _customerService.GetPagos();
            foreach (var pago in pagos)
            {
                var demografico = demograficos.FirstOrDefault(d => d.ID == pago.ID);
                if (demografico != null)
                {
                    pago.Nombre = demografico.Nombre;
                    pago.NombreComercial = demografico.NombreComercial;
                }
            }

            var confidenciales = _customerService.GetConfidenciales();
            foreach (var confidencial in confidenciales)
            {
                var demografico = demograficos.FirstOrDefault(d => d.ID == confidencial.ID);
                if (demografico != null)
                {
                    confidencial.Nombre = demografico.Nombre;
                    confidencial.NombreComercial = demografico.NombreComercial;
                }
            }

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

            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            var contributivoViewModel = new
            {
                contributivo.ID,
                Nombre = demografico.Nombre,
                NombreComercial = demografico.NombreComercial,
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

                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);

                if (existingContributivo != null && existingDemografico != null)
                {

                    existingContributivo.Estatal = model.Estatal;
                    existingContributivo.Poliza = model.Poliza;
                    existingContributivo.RegComerciante = model.RegComerciante;
                    existingContributivo.Vencimiento = model.Vencimiento;
                    existingContributivo.Choferil = model.Choferil;
                    existingContributivo.DeptEstado = model.DeptEstado;
                    existingContributivo.CID = model.CID;
                    existingContributivo.MID = model.MID;

                    _customerService.UpdateContributivo(existingContributivo);

                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    _customerService.UpdateDemografico(existingDemografico);

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

            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            // Convert the date fields to a string in the proper format (yyyy-MM-ddTHH:mm)
            var administrativoViewModel = new
            {
                administrativo.ID,
                Nombre = demografico.Nombre,
                NombreComercial = demografico.NombreComercial,
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
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);

                if (existingAdministrativo != null && existingDemografico != null)
                {

                    existingAdministrativo.Contrato = model.Contrato;
                    existingAdministrativo.Facturacion = model.Facturacion;
                    existingAdministrativo.FacturacionBase = model.FacturacionBase;
                    existingAdministrativo.IVU = model.IVU;
                    existingAdministrativo.Staff = model.Staff;
                    existingAdministrativo.StaffDate = model.StaffDate;
                    existingAdministrativo.CID = model.CID;
                    existingAdministrativo.MID = model.MID;

                    _customerService.UpdateAdministrativo(existingAdministrativo);

                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    _customerService.UpdateDemografico(existingDemografico);

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

            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            var identificacionViewModel = new
            {
                identificacion.ID,
                Nombre = demografico.Nombre,
                NombreComercial = demografico.NombreComercial,
                identificacion.Accionista,
                identificacion.SSNA,
                identificacion.Cargo,
                identificacion.LicConducir,
                Nacimiento = identificacion.Nacimiento.ToString("yyyy-MM-dd"),
                identificacion.CID,
                identificacion.MID
            };

            return Json(identificacionViewModel);
        }

        [HttpPost]
        public IActionResult UpdateIdentificacion(Identificacion model)
        {
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingIdentificacion = _customerService.GetIdentificaciones().FirstOrDefault(i => i.ID == model.ID);
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);
                if (existingIdentificacion!= null && existingDemografico != null)
                {
                    existingIdentificacion.Accionista = model.Accionista;
                    existingIdentificacion.SSNA = model.SSNA;
                    existingIdentificacion.Cargo = model.Cargo;
                    existingIdentificacion.LicConducir = model.LicConducir;
                    existingIdentificacion.Nacimiento = model.Nacimiento;
                    existingIdentificacion.CID = model.CID;
                    existingIdentificacion.MID = model.MID;

                    _customerService.UpdateIdentificacion(existingIdentificacion);

                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    _customerService.UpdateDemografico(existingDemografico);

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Identificacion not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

        [HttpGet]
        public IActionResult GetPagoById(string id)
        {
            var pago = _customerService.GetPagos().FirstOrDefault(p => p.ID == id);
            if (pago == null)
            {
                return NotFound();
            }

            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            // Convert the date fields to a string in the proper format (yyyy-MM-ddTHH:mm)
            var pagoViewModel = new
            {
                pago.ID,
                Nombre = demografico.Nombre,
                NombreComercial = demografico.NombreComercial,
                pago.BankClient,
                pago.Banco,
                pago.NumRuta,
                pago.NameBank,
                pago.TipoCuenta,
                pago.BankClientS,
                pago.BancoS,
                pago.NumRutaS,
                pago.NameBankS,
                pago.TipoCuentaS,
                pago.NameCard,
                pago.Tarjeta,
                pago.TipoTarjeta,
                pago.CVV,
                pago.Expiracion,
                pago.PostalBank,
                pago.NameCardS,
                pago.TarjetaS,
                pago.TipoTarjetaS,
                pago.CVVS,
                pago.ExpiracionS,
                pago.PostalBankS,
                pago.CID,
                pago.MID
            };

            return Json(pagoViewModel);
        }

        [HttpPost]
        public IActionResult UpdatePago(Pago model)
        {
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingPago = _customerService.GetPagos().FirstOrDefault(p => p.ID == model.ID);
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);

                if (existingPago != null && existingDemografico != null)
                {
                    existingPago.BankClient = model.BankClient;
                    existingPago.Banco = model.Banco;
                    existingPago.NumRuta = model.NumRuta;
                    existingPago.NameBank = model.NameBank;
                    existingPago.TipoCuenta = model.TipoCuenta;
                    existingPago.BankClientS = model.BankClientS;
                    existingPago.BancoS = model.BancoS;
                    existingPago.NumRutaS = model.NumRutaS;
                    existingPago.NameBankS = model.NameBankS;
                    existingPago.TipoCuentaS = model.TipoCuentaS;
                    existingPago.NameCard = model.NameCard;
                    existingPago.Tarjeta = model.Tarjeta;
                    existingPago.TipoTarjeta = model.TipoTarjeta;
                    existingPago.CVV = model.CVV;
                    existingPago.Expiracion = model.Expiracion;
                    existingPago.PostalBank = model.PostalBank;
                    existingPago.NameCardS = model.NameCardS;
                    existingPago.TarjetaS = model.TarjetaS;
                    existingPago.TipoTarjetaS = model.TipoTarjetaS;
                    existingPago.CVVS = model.CVVS;
                    existingPago.ExpiracionS = model.ExpiracionS;
                    existingPago.PostalBankS = model.PostalBankS;
                    existingPago.CID = model.CID;
                    existingPago.MID = model.MID;

                    _customerService.UpdatePago(existingPago);

                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    _customerService.UpdateDemografico(existingDemografico);

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Pago not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

        [HttpGet]
        public IActionResult GetConfidencialById(string id)
        {
            var confidencial = _customerService.GetConfidenciales().FirstOrDefault(c => c.ID == id);
            if (confidencial == null)
            {
                return NotFound();
            }

            var demografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);
            if (demografico == null)
            {
                return NotFound();
            }

            var confidencialViewModel = new
            {
                confidencial.ID,
                Nombre = demografico.Nombre,
                NombreComercial = demografico.NombreComercial,
                confidencial.UserSuri,
                confidencial.PassSuri,
                confidencial.UserEftps,
                confidencial.PassEftps,
                confidencial.PIN,
                confidencial.UserCFSE,
                confidencial.PassCFSE,
                confidencial.UserDept,
                confidencial.PassDept,
                confidencial.UserCofim,
                confidencial.PassCofim,
                confidencial.UserMunicipio,
                confidencial.PassMunicipio,
                confidencial.CID,
                confidencial.MID
            };

            return Json(confidencialViewModel);
        }

        [HttpPost]
        public IActionResult UpdateConfidencial(Confidencial model)
        {
            ModelState.Remove(nameof(model.Registro));

            if (ModelState.IsValid)
            {
                var existingConfidencial = _customerService.GetConfidenciales().FirstOrDefault(c => c.ID == model.ID);
                var existingDemografico = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == model.ID);


                if (existingConfidencial != null && existingDemografico != null)
                {
                    existingConfidencial.UserSuri = model.UserSuri;
                    existingConfidencial.PassSuri = model.PassSuri;
                    existingConfidencial.UserEftps = model.UserEftps;
                    existingConfidencial.PassEftps = model.PassEftps;
                    existingConfidencial.PIN = model.PIN;
                    existingConfidencial.UserCFSE = model.UserCFSE;
                    existingConfidencial.PassCFSE = model.PassCFSE;
                    existingConfidencial.UserDept = model.UserDept;
                    existingConfidencial.PassDept = model.PassDept;
                    existingConfidencial.UserCofim = model.UserCofim;
                    existingConfidencial.PassCofim = model.PassCofim;
                    existingConfidencial.UserMunicipio = model.UserMunicipio;
                    existingConfidencial.PassMunicipio = model.PassMunicipio;
                    existingConfidencial.CID = model.CID;
                    existingConfidencial.MID = model.MID;
                    _customerService.UpdateConfidencial(existingConfidencial);

                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    _customerService.UpdateDemografico(existingDemografico);

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Confidencial not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }

    }
}
