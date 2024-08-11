using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using BLL.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly ExcelExportService _excelExportService;
    private readonly ICustomerService _customerService;

    public ExportController(ExcelExportService excelExportService, ICustomerService customerService)
    {
        _excelExportService = excelExportService;
        _customerService = customerService;
    }

    [HttpGet("to-excel")]
    public IActionResult ExportToExcel(string customerId)
    {
        // Get the customer by ID to ensure it's the correct one
        var customer = _customerService.GetDemograficos().FirstOrDefault(d => d.ID == customerId);

        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        // Retrieve all related data based on the customer ID
        var demograficos = new List<Demografico> { customer };
        var contributivos = _customerService.GetContributivos().Where(c => c.ID == customerId).ToList();
        var administrativos = _customerService.GetAdministrativos().Where(a => a.ID == customerId).ToList();
        var identificaciones = _customerService.GetIdentificaciones().Where(i => i.ID == customerId).ToList();
        var pagos = _customerService.GetPagos().Where(p => p.ID == customerId).ToList();
        var confidenciales = _customerService.GetConfidenciales().Where(c => c.ID == customerId).ToList();

        var excelFile = _excelExportService.ExportToExcel(demograficos, contributivos, administrativos, identificaciones, pagos, confidenciales);

        return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerData.xlsx");
    }
}