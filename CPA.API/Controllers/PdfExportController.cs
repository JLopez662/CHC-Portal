using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using BLL.Interfaces;

[ApiController]
[Route("api/export")]
public class PdfExportController : ControllerBase
{
    private readonly PdfExportService _pdfExportService;
    private readonly ICustomerService _customerService;

    public PdfExportController(PdfExportService pdfExportService, ICustomerService customerService)
    {
        _pdfExportService = pdfExportService;
        _customerService = customerService;
    }

    [HttpGet("to-pdf")]
    public IActionResult ExportToPdf(string customerId)
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

        var pdfFile = _pdfExportService.ExportToPdf(demograficos, contributivos, administrativos, identificaciones, pagos, confidenciales);

        // Create the filename using the customer's name and the current date (yyyyMMdd format)
        var currentDate = DateTime.Now.ToString("yyyyMMdd");
        var filename = $"{customer.Nombre}_Data_{currentDate}.pdf";

        return File(pdfFile, "application/pdf", filename);
    }
}
