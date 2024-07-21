using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;
using CPA.Models;
using System.Linq;
using BLL.Interfaces;
using System;
using System.Diagnostics;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Logging;


namespace CPA.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IRegistroService _registroService;

        public DashboardController(ICustomerService customerService, IRegistroService registroService)
        {
            _customerService = customerService;
            _registroService = registroService;
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
                    // Update properties
                    existingDemografico.Nombre = model.Nombre;
                    existingDemografico.NombreComercial = model.NombreComercial;
                    // Update other properties...

                    _customerService.UpdateDemografico(existingDemografico);
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Demografico not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Model validation failed.", errors });
        }
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
                contributivo.Vencimiento,
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
                identificacion.Nacimiento,
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
                if (existingIdentificacion != null && existingDemografico != null)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DashboardViewModel model)
        {
            try
            {
                // Remove unnecessary validation
                ModelState.Remove("NewDemografico.Registro");
                ModelState.Remove("NewContributivo.Registro");
                ModelState.Remove("NewAdministrativo.Registro");
                ModelState.Remove("NewIdentificacion.Registro");
                ModelState.Remove("NewPago.Registro");
                ModelState.Remove("NewConfidencial.Registro");

                if (ModelState.IsValid)
                {
                    // Create a new Registro and get the generated ID
                    var registro = new Registro();
                    _registroService.CreateRegistro(registro);

                    var registroId = registro.ID;

                    // Variables to hold Nombre and NombreComercial
                    string nombre = null;
                    string nombreComercial = null;

                    // Process NewDemografico
                    if (model.NewDemografico != null)
                    {
                        model.NewDemografico.ID = registroId;
                        _customerService.CreateDemografico(model.NewDemografico);
                        nombre = model.NewDemografico.Nombre ?? string.Empty;
                        nombreComercial = model.NewDemografico.NombreComercial ?? string.Empty;
                    }

                    // Process NewContributivo
                    if (model.NewContributivo != null)
                    {
                        var contributivo = model.NewContributivo;
                        contributivo.ID = registroId;
                        contributivo.Nombre = nombre;
                        contributivo.NombreComercial = nombreComercial;

                        // Log the values before creating the Contributivo
                        Console.WriteLine($"Creating Contributivo with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}, Estatal: {contributivo.Estatal}");
                        Debug.WriteLine($"Creating Contributivo with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}, Estatal: {contributivo.Estatal}");

                        _customerService.CreateContributivo(contributivo);
                    }

                    // Process NewAdministrativo
                    if (model.NewAdministrativo != null)
                    {
                        var administrativo = model.NewAdministrativo;
                        administrativo.ID = registroId;
                        administrativo.Nombre = nombre;
                        administrativo.NombreComercial = nombreComercial;

                        // Log the values before creating the Administrativo
                        Console.WriteLine($"Creating Administrativo with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");
                        Debug.WriteLine($"Creating Administrativo with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");

                        _customerService.CreateAdministrativo(administrativo);
                    }

                    // Process NewIdentificacion
                    if (model.NewIdentificacion != null)
                    {
                        var identificacion = model.NewIdentificacion;
                        identificacion.ID = registroId;
                        identificacion.Nombre = nombre;
                        identificacion.NombreComercial = nombreComercial;

                        // Log the values before creating the Identificacion
                        Console.WriteLine($"Creating Identificacion with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");
                        Debug.WriteLine($"Creating Identificacion with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");

                        _customerService.CreateIdentificacion(identificacion);
                    }

                    // Process NewPago
                    if (model.NewPago != null)
                    {
                        var pago = model.NewPago;
                        pago.ID = registroId;
                        pago.Nombre = nombre;
                        pago.NombreComercial = nombreComercial;

                        // Log the values before creating the Pago
                        Console.WriteLine($"Creating Pago with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");
                        Debug.WriteLine($"Creating Pago with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");

                        _customerService.CreatePago(pago);
                    }

                    // Process NewConfidencial
                    if (model.NewConfidencial != null)
                    {
                        var confidencial = model.NewConfidencial;
                        confidencial.ID = registroId;
                        confidencial.Nombre = nombre;
                        confidencial.NombreComercial = nombreComercial;

                        // Log the values before creating the Confidencial
                        Console.WriteLine($"Creating Confidencial with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");
                        Debug.WriteLine($"Creating Confidencial with ID: {registroId}, Nombre: {nombre}, NombreComercial: {nombreComercial}");

                        _customerService.CreateConfidencial(confidencial);
                    }

                    return Json(new { success = true, message = "Customer records created successfully!" });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Model validation failed.", errors });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer records: {ex.Message}");
                Debug.WriteLine($"Error creating customer records: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult CreateOtherEntities(DashboardViewModel model)
        {
            try
            {
                var demograficoResult = CreateDemograficoInternal(model.NewDemografico);
                if (!demograficoResult.success)
                {
                    return Json(new { success = false, message = demograficoResult.message, errors = demograficoResult.errors });
                }

                var newDemografico = demograficoResult.demografico;
                model.NewDemografico = newDemografico;

                Console.WriteLine("NewDemografico is not null");
                Console.WriteLine($"ID: {model.NewDemografico.ID}");
                Console.WriteLine($"Nombre: {model.NewDemografico.Nombre}");
                Console.WriteLine($"NombreComercial: {model.NewDemografico.NombreComercial}");

                var registroId = model.NewDemografico.ID;
                var nombre = model.NewDemografico.Nombre;
                var nombreComercial = model.NewDemografico.NombreComercial;

                if (model.NewContributivo != null)
                {
                    var contributivo = model.NewContributivo;
                    contributivo.ID = registroId;
                    contributivo.Nombre = nombre;
                    contributivo.NombreComercial = nombreComercial;
                    _customerService.CreateContributivo(contributivo);
                }

                if (model.NewAdministrativo != null)
                {
                    var administrativo = model.NewAdministrativo;
                    administrativo.ID = registroId;
                    administrativo.Nombre = nombre;
                    administrativo.NombreComercial = nombreComercial;
                    _customerService.CreateAdministrativo(administrativo);
                }

                if (model.NewIdentificacion != null)
                {
                    var identificacion = model.NewIdentificacion;
                    identificacion.ID = registroId;
                    identificacion.Nombre = nombre;
                    identificacion.NombreComercial = nombreComercial;
                    _customerService.CreateIdentificacion(identificacion);
                }

                if (model.NewPago != null)
                {
                    var pago = model.NewPago;
                    pago.ID = registroId;
                    pago.Nombre = nombre;
                    pago.NombreComercial = nombreComercial;
                    _customerService.CreatePago(pago);
                }

                if (model.NewConfidencial != null)
                {
                    var confidencial = model.NewConfidencial;
                    confidencial.ID = registroId;
                    confidencial.Nombre = nombre;
                    confidencial.NombreComercial = nombreComercial;
                    _customerService.CreateConfidencial(confidencial);
                }

                return Json(new { success = true, message = "Customer records created successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public IActionResult CreateDemografico(Demografico newDemografico)
        {
            var result = CreateDemograficoInternal(newDemografico);
            if (result.success)
            {
                return Json(new { success = true, registroId = result.demografico.ID });
            }
            return Json(new { success = false, message = result.message, errors = result.errors });
        }


        private (bool success, string message, List<string> errors, Demografico demografico) CreateDemograficoInternal(Demografico newDemografico)
        {
            try
            {
                if (newDemografico == null)
                {
                    return (false, "New Demografico object is null.", null, null);
                }

                if (ModelState.IsValid)
                {
                    var registro = new Registro();
                    _registroService.CreateRegistro(registro);

                    newDemografico.Registro = registro;
                    newDemografico.ID = registro.ID;

                    _customerService.CreateDemografico(newDemografico);

                    return (true, null, null, newDemografico);
                }

                var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return (false, "Model validation failed.", errorList, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, null);
            }
        }




        [HttpPost]
        public IActionResult CreateContributivo(Contributivo newContributivo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Log the incoming data to ensure it's being passed correctly
                    Debug.WriteLine($"Creating Contributivo: ID = {newContributivo.ID}, Estatal = {newContributivo.Estatal}");
                    Console.WriteLine($"Creating Contributivo: ID = {newContributivo.ID}, Estatal = {newContributivo.Estatal}");

                    _customerService.CreateContributivo(newContributivo);
                    return Json(new { success = true });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Model validation failed.", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult CreateAdministrativo(Administrativo newAdministrativo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _customerService.CreateAdministrativo(newAdministrativo);
                    return Json(new { success = true });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Model validation failed.", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteCustomer(string id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return Json(new { success = true, message = "Customer deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
