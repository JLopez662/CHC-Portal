using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Models;
using CPA.Services;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class ImportController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IRegistroService _registroService;
    private readonly ExcelImportService _excelImportService;
    private readonly ILogger<ImportController> _logger;

    public ImportController(ICustomerService customerService, IRegistroService registroService, ExcelImportService excelImportService, ILogger<ImportController> logger)
    {
        _customerService = customerService;
        _registroService = registroService;
        _excelImportService = excelImportService;
        _logger = logger;
    }

    [HttpPost("from-excel")]
    public async Task<IActionResult> ImportFromExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { success = false, message = "No file uploaded or the file is empty." });
        }

        try
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                // Use the ExcelImportService to process the file, from each excel sheet

                DataTable demograficoTable = _excelImportService.ImportExcel(stream, 0);

                DataTable contributivoTable = _excelImportService.ImportExcel(stream, 1); 

                DataTable administrativoTable = _excelImportService.ImportExcel(stream, 2);

                DataTable identificacionTable = _excelImportService.ImportExcel(stream, 3); 

                DataTable pagoTable = _excelImportService.ImportExcel(stream, 4); 

                DataTable confidencialTable = _excelImportService.ImportExcel(stream, 5); 

                // Debugging: Print out the rows in the Demographic DataTable
                foreach (DataRow row in demograficoTable.Rows)
                {
                    _logger.LogInformation($"{row["Field"]}: {row["Value"]}");
                }

                // Get the ID from the demographic data
                var id = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "ID")?["Value"].ToString();

                // Check if a Demografico with this ID already exists
                var existingCustomer = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == id);


                // If no existing Demografico, create a new one
                if (existingCustomer == null)
                {
                    // Create a new Registro entity
                    var registro = new Registro { ID = id };
                    _registroService.CreateRegistro(registro);

                    // Create a new Demografico entity and associate it with the new Registro
                    var customer = new Demografico
                    {
                        ID = id,
                        Registro = registro,
                        Nombre = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                        NombreComercial = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                        Telefono = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Phone Number")?["Value"].ToString(),
                        Celular = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Mobile Number")?["Value"].ToString(),
                        Dir = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Address")?["Value"].ToString(),
                        Tipo = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Business Type")?["Value"].ToString(),
                        Patronal = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Federal Employer Identification Number")?["Value"].ToString(),
                        SSN = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString(),
                        Incorporacion = DateTime.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Date of Incorporation")?["Value"].ToString(), out DateTime incDate) ? (DateTime?)incDate : null,
                        Operaciones = DateTime.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Operations Start Date")?["Value"].ToString(), out DateTime opDate) ? (DateTime?)opDate : null,
                        Industria = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Industry")?["Value"].ToString(),
                        NAICS = int.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "NAICS Code")?["Value"].ToString(), out int naicsValue) ? (int?)naicsValue : null,
                        Descripcion = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Description")?["Value"].ToString(),
                        Contacto = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Contact")?["Value"].ToString(),
                        DirFisica = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Physical Address")?["Value"].ToString(),
                        DirPostal = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address")?["Value"].ToString(),
                        Email = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Email")?["Value"].ToString(),
                        Email2 = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Email")?["Value"].ToString(),
                    };

                    _customerService.CreateDemografico(customer);
                }
                else
                {
                    // Update existing customer details
                    existingCustomer.Nombre = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingCustomer.NombreComercial = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingCustomer.Telefono = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Phone Number")?["Value"].ToString();
                    existingCustomer.Celular = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Mobile Number")?["Value"].ToString();
                    existingCustomer.Dir = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Address")?["Value"].ToString();
                    existingCustomer.Tipo = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Business Type")?["Value"].ToString();
                    existingCustomer.Patronal = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Federal Employer Identification Number")?["Value"].ToString();
                    existingCustomer.SSN = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString();
                    existingCustomer.Incorporacion = DateTime.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Date of Incorporation")?["Value"].ToString(), out DateTime incDate) ? (DateTime?)incDate : null;
                    existingCustomer.Operaciones = DateTime.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Operations Start Date")?["Value"].ToString(), out DateTime opDate) ? (DateTime?)opDate : null;
                    existingCustomer.Industria = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Industry")?["Value"].ToString();
                    existingCustomer.NAICS = int.TryParse(demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "NAICS Code")?["Value"].ToString(), out int naicsValue) ? (int?)naicsValue : null;
                    existingCustomer.Descripcion = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Description")?["Value"].ToString();
                    existingCustomer.Contacto = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Contact")?["Value"].ToString();
                    existingCustomer.DirFisica = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Physical Address")?["Value"].ToString();
                    existingCustomer.DirPostal = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address")?["Value"].ToString();
                    existingCustomer.Email = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Email")?["Value"].ToString();
                    existingCustomer.Email2 = demograficoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Email")?["Value"].ToString();

                    _customerService.UpdateDemografico(existingCustomer);
                }


                // Handling tax matters (Contributivo)
                var contributivo = new Contributivo
                {
                    ID = id,
                    Nombre = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                    NombreComercial = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                    Estatal = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "State Identification Number")?["Value"].ToString(),
                    Poliza = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "CFSE Policy Number")?["Value"].ToString(),
                    RegComerciante = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Merchant Registry Number")?["Value"].ToString(),
                    Vencimiento = DateTime.TryParse(contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Expiration Date")?["Value"].ToString(), out DateTime expDate) ? (DateTime?)expDate : null,
                    Choferil = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Chauffeur's Insurance Account Number")?["Value"].ToString(),
                    DeptEstado = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "State Department Registry Number")?["Value"].ToString(),
                };

                var existingContributivo = _customerService.GetContributivos().FirstOrDefault(c => c.ID == id);

                if (existingContributivo == null)
                {
                    // Add new Contributivo
                    _customerService.CreateContributivo(contributivo);
                }
                else
                {
                    // Update existing Contributivo
                    existingContributivo.Nombre = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingContributivo.NombreComercial = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingContributivo.Estatal = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "State Identification Number")?["Value"].ToString();
                    existingContributivo.Poliza = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "CFSE Policy Number")?["Value"].ToString();
                    existingContributivo.RegComerciante = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Merchant Registry Number")?["Value"].ToString();
                    existingContributivo.Vencimiento = DateTime.TryParse(contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Expiration Date")?["Value"].ToString(), out DateTime expDateVencimiento) ? (DateTime?)expDateVencimiento : null;
                    existingContributivo.Choferil = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Chauffeur's Insurance Account Number")?["Value"].ToString();
                    existingContributivo.DeptEstado = contributivoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "State Department Registry Number")?["Value"].ToString();

                    _customerService.UpdateContributivo(existingContributivo);
                }

                var administrativo = new Administrativo
                {
                    ID = id,
                    Nombre = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                    NombreComercial = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                    Contrato = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Hourly Billing Rate")?["Value"].ToString(),
                    Facturacion = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Monthly Billing Rate")?["Value"].ToString(),
                    IVU = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "IVU")?["Value"].ToString(),
                    Staff = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Staff")?["Value"].ToString(),
                    StaffDate = DateTime.TryParse(administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Staff Assignment Date")?["Value"].ToString(), out DateTime staffDate) ? (DateTime?)staffDate : null,
                };

                var existingAdministrativo = _customerService.GetAdministrativos().FirstOrDefault(a => a.ID == id);

                if (existingAdministrativo == null)
                {
                    // Add new Administrativo
                    _customerService.CreateAdministrativo(administrativo);
                }
                else
                {
                    // Update existing Administrativo
                    existingAdministrativo.Nombre = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingAdministrativo.NombreComercial = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingAdministrativo.Contrato = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Hourly Billing Rate")?["Value"].ToString();
                    existingAdministrativo.Facturacion = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Monthly Billing Rate")?["Value"].ToString();
                    existingAdministrativo.IVU = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "IVU")?["Value"].ToString();
                    existingAdministrativo.Staff = administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Staff")?["Value"].ToString();
                    existingAdministrativo.StaffDate = DateTime.TryParse(administrativoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Staff Assignment Date")?["Value"].ToString(), out DateTime staffDateUpdate) ? (DateTime?)staffDateUpdate : null;

                    _customerService.UpdateAdministrativo(existingAdministrativo);
                }

                var identificacion = new Identificacion
                {
                    ID = id,
                    Nombre = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                    NombreComercial = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                    Accionista = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Shareholder / Owner")?["Value"].ToString(),
                    SSNA = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString(),
                    Cargo = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Title")?["Value"].ToString(),
                    LicConducir = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Driver's License")?["Value"].ToString(),
                    Nacimiento = DateTime.TryParse(identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Birth Date")?["Value"].ToString(), out DateTime birthDate) ? (DateTime?)birthDate : null,
                };

                var existingIdentificacion = _customerService.GetIdentificaciones().FirstOrDefault(i => i.ID == id);

                if (existingIdentificacion == null)
                {
                    // Add new Identificacion
                    _customerService.CreateIdentificacion(identificacion);
                }
                else
                {
                    // Update existing Identificacion
                    existingIdentificacion.Nombre = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingIdentificacion.NombreComercial = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingIdentificacion.Accionista = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Shareholder / Owner")?["Value"].ToString();
                    existingIdentificacion.SSNA = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString();
                    existingIdentificacion.Cargo = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Title")?["Value"].ToString();
                    existingIdentificacion.LicConducir = identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Driver's License")?["Value"].ToString();
                    existingIdentificacion.Nacimiento = DateTime.TryParse(identificacionTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Birth Date")?["Value"].ToString(), out DateTime birthDateUpdate) ? (DateTime?)birthDateUpdate : null;

                    _customerService.UpdateIdentificacion(existingIdentificacion);
                }

                var pago = new Pago
                {
                    ID = id,
                    Nombre = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                    NombreComercial = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                    BankClient = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Customer's Name in Bank")?["Value"].ToString(),
                    Banco = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Bank Account")?["Value"].ToString(),
                    NumRuta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Routing Number")?["Value"].ToString(),
                    NameBank = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Bank's Name")?["Value"].ToString(),
                    TipoCuenta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Account Type")?["Value"].ToString(),
                    BankClientS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Customer's Name in Bank")?["Value"].ToString(),
                    BancoS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Bank Account")?["Value"].ToString(),
                    NumRutaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Routing Number")?["Value"].ToString(),
                    NameBankS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Bank's Name")?["Value"].ToString(),
                    TipoCuentaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Account Type")?["Value"].ToString(),
                    NameCard = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Customer's Name in Card")?["Value"].ToString(),
                    Tarjeta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Credit Card Number")?["Value"].ToString(),
                    TipoTarjeta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Type of Card")?["Value"].ToString(),
                    CVV = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "CVV")?["Value"].ToString(),
                    Expiracion = DateTime.TryParse(pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Expiration Date")?["Value"].ToString(), out DateTime expDateCard) ? (DateTime?)expDateCard : null,
                    PostalBank = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address in Card")?["Value"].ToString(),
                    NameCardS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Customer's Name in Card")?["Value"].ToString(),
                    TarjetaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Credit Card Number")?["Value"].ToString(),
                    TipoTarjetaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Type of Card")?["Value"].ToString(),
                    CVVS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary CVV")?["Value"].ToString(),
                    ExpiracionS = DateTime.TryParse(pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Expiration Date")?["Value"].ToString(), out DateTime expDateS) ? (DateTime?)expDateS : null,
                    PostalBankS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Postal Address in Card")?["Value"].ToString(),
                };

                var existingPago = _customerService.GetPagos().FirstOrDefault(p => p.ID == id);

                if (existingPago == null)
                {
                    // Add new Pago
                    _customerService.CreatePago(pago);
                }
                else
                {
                    // Update existing Pago
                    existingPago.Nombre = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingPago.NombreComercial = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingPago.BankClient = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Customer's Name in Bank")?["Value"].ToString();
                    existingPago.Banco = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Bank Account")?["Value"].ToString();
                    existingPago.NumRuta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Routing Number")?["Value"].ToString();
                    existingPago.NameBank = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Bank's Name")?["Value"].ToString();
                    existingPago.TipoCuenta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Account Type")?["Value"].ToString();
                    existingPago.BankClientS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Customer's Name in Bank")?["Value"].ToString();
                    existingPago.BancoS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Bank Account")?["Value"].ToString();
                    existingPago.NumRutaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Routing Number")?["Value"].ToString();
                    existingPago.NameBankS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Bank's Name")?["Value"].ToString();
                    existingPago.TipoCuentaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Account Type")?["Value"].ToString();
                    existingPago.NameCard = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Customer's Name in Card")?["Value"].ToString();
                    existingPago.Tarjeta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Credit Card Number")?["Value"].ToString();
                    existingPago.TipoTarjeta = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Type of Card")?["Value"].ToString();
                    existingPago.CVV = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "CVV")?["Value"].ToString();
                    existingPago.Expiracion = DateTime.TryParse(pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Expiration Date")?["Value"].ToString(), out DateTime expDateUpdate) ? (DateTime?)expDateUpdate : null;
                    existingPago.PostalBank = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address in Card")?["Value"].ToString();
                    existingPago.NameCardS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Customer's Name in Card")?["Value"].ToString();
                    existingPago.TarjetaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Credit Card Number")?["Value"].ToString();
                    existingPago.TipoTarjetaS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Type of Card")?["Value"].ToString();
                    existingPago.CVVS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary CVV")?["Value"].ToString();
                    existingPago.ExpiracionS = DateTime.TryParse(pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Expiration Date")?["Value"].ToString(), out DateTime expDateUpdateS) ? (DateTime?)expDateUpdateS : null;
                    existingPago.PostalBankS = pagoTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Postal Address in Card")?["Value"].ToString();

                    _customerService.UpdatePago(existingPago);
                }

                // Handling confidential information (Confidencial)
                var confidencial = new Confidencial
                {
                    ID = id,
                    Nombre = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                    NombreComercial = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                    UserSuri = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Suri")?["Value"].ToString(),
                    PassSuri = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Suri")?["Value"].ToString(),
                    UserEftps = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Eftps")?["Value"].ToString(),
                    PassEftps = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Internet Password Eftps")?["Value"].ToString(),
                    PIN = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "PIN")?["Value"].ToString(),
                    UserCFSE = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username CFSE")?["Value"].ToString(),
                    PassCFSE = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password CFSE")?["Value"].ToString(),
                    UserDept = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Department of Labor")?["Value"].ToString(),
                    PassDept = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Department of Labor")?["Value"].ToString(),
                    UserCofim = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Cofim")?["Value"].ToString(),
                    PassCofim = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Cofim")?["Value"].ToString(),
                    UserMunicipio = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Municipality")?["Value"].ToString(),
                    PassMunicipio = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Municipality")?["Value"].ToString(),
                };

                var existingConfidencial = _customerService.GetConfidenciales().FirstOrDefault(c => c.ID == id);

                if (existingConfidencial == null)
                {
                    // Add new Confidencial
                    _customerService.CreateConfidencial(confidencial);
                }
                else
                {
                    // Update existing Confidencial
                    existingConfidencial.Nombre = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingConfidencial.NombreComercial = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingConfidencial.UserSuri = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Suri")?["Value"].ToString();
                    existingConfidencial.PassSuri = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Suri")?["Value"].ToString();
                    existingConfidencial.UserEftps = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Eftps")?["Value"].ToString();
                    existingConfidencial.PassEftps = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Internet Password Eftps")?["Value"].ToString();
                    existingConfidencial.PIN = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "PIN")?["Value"].ToString();
                    existingConfidencial.UserCFSE = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username CFSE")?["Value"].ToString();
                    existingConfidencial.PassCFSE = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password CFSE")?["Value"].ToString();
                    existingConfidencial.UserDept = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Department of Labor")?["Value"].ToString();
                    existingConfidencial.PassDept = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Department of Labor")?["Value"].ToString();
                    existingConfidencial.UserCofim = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Cofim")?["Value"].ToString();
                    existingConfidencial.PassCofim = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Cofim")?["Value"].ToString();
                    existingConfidencial.UserMunicipio = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Username Municipality")?["Value"].ToString();
                    existingConfidencial.PassMunicipio = confidencialTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Password Municipality")?["Value"].ToString();

                    _customerService.UpdateConfidencial(existingConfidencial);
                }


                return Ok(new { success = true, message = "Customers imported successfully!" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the import process");
            return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
        }
    }
}
