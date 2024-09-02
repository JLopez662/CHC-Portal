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

                // Use the ExcelImportService to process the file
                DataTable dataTable = _excelImportService.ImportExcel(stream);

                // Debugging: Print out the rows in the DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    _logger.LogInformation($"{row["Field"]}: {row["Value"]}");
                }

                // Get the ID from the Excel data
                var id = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "ID")?["Value"].ToString();

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
                        Nombre = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString(),
                        NombreComercial = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString(),
                        Telefono = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Phone Number")?["Value"].ToString(),
                        Celular = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Mobile Number")?["Value"].ToString(),
                        Dir = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Address")?["Value"].ToString(),
                        Tipo = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Business Type")?["Value"].ToString(),
                        Patronal = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Federal Employer Identification Number")?["Value"].ToString(),
                        SSN = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString(),
                        Incorporacion = DateTime.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Date of Incorporation")?["Value"].ToString(), out DateTime incDate) ? (DateTime?)incDate : null,
                        Operaciones = DateTime.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Operations Start Date")?["Value"].ToString(), out DateTime opDate) ? (DateTime?)opDate : null,
                        Industria = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Industry")?["Value"].ToString(),
                        NAICS = int.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "NAICS Code")?["Value"].ToString(), out int naicsValue) ? (int?)naicsValue : null,
                        Descripcion = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Description")?["Value"].ToString(),
                        Contacto = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Contact")?["Value"].ToString(),
                        DirFisica = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Physical Address")?["Value"].ToString(),
                        DirPostal = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address")?["Value"].ToString(),
                        Email = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Email")?["Value"].ToString(),
                        Email2 = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Email")?["Value"].ToString(),
                    };

                    _customerService.CreateDemografico(customer);
                }
                else
                {
                    // Update existing customer details
                    existingCustomer.Nombre = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Name")?["Value"].ToString();
                    existingCustomer.NombreComercial = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Commercial Name")?["Value"].ToString();
                    existingCustomer.Telefono = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Phone Number")?["Value"].ToString();
                    existingCustomer.Celular = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Mobile Number")?["Value"].ToString();
                    existingCustomer.Dir = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Address")?["Value"].ToString();
                    existingCustomer.Tipo = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Business Type")?["Value"].ToString();
                    existingCustomer.Patronal = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Federal Employer Identification Number")?["Value"].ToString();
                    existingCustomer.SSN = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Social Security Number")?["Value"].ToString();
                    existingCustomer.Incorporacion = DateTime.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Date of Incorporation")?["Value"].ToString(), out DateTime incDate) ? (DateTime?)incDate : null;
                    existingCustomer.Operaciones = DateTime.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Operations Start Date")?["Value"].ToString(), out DateTime opDate) ? (DateTime?)opDate : null;
                    existingCustomer.Industria = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Industry")?["Value"].ToString();
                    existingCustomer.NAICS = int.TryParse(dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "NAICS Code")?["Value"].ToString(), out int naicsValue) ? (int?)naicsValue : null;
                    existingCustomer.Descripcion = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Description")?["Value"].ToString();
                    existingCustomer.Contacto = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Contact")?["Value"].ToString();
                    existingCustomer.DirFisica = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Physical Address")?["Value"].ToString();
                    existingCustomer.DirPostal = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Postal Address")?["Value"].ToString();
                    existingCustomer.Email = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Email")?["Value"].ToString();
                    existingCustomer.Email2 = dataTable.Rows.OfType<DataRow>().FirstOrDefault(r => r["Field"].ToString() == "Secondary Email")?["Value"].ToString();

                    _customerService.UpdateDemografico(existingCustomer);
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
