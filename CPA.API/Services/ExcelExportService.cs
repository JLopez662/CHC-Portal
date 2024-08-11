using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using DAL.Models;

public class ExcelExportService
{
    public byte[] ExportToExcel(
        List<Demografico> demograficos,
        List<Contributivo> contributivos,
        List<Administrativo> administrativos,
        List<Identificacion> identificaciones,
        List<Pago> pagos,
        List<Confidencial> confidenciales)

    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            // Demograficos Sheet
            var demograficosSheet = package.Workbook.Worksheets.Add("Demograficos");
            AddDemograficosData(demograficosSheet, demograficos);

            // Contributivos Sheet
            var contributivosSheet = package.Workbook.Worksheets.Add("Contributivos");
            AddContributivosData(contributivosSheet, contributivos);

            // Administrativos Sheet
            var administrativosSheet = package.Workbook.Worksheets.Add("Administrativos");
            AddAdministrativosData(administrativosSheet, administrativos);

            // Identificaciones Sheet
            var identificacionesSheet = package.Workbook.Worksheets.Add("Identificaciones");
            AddIdentificacionesData(identificacionesSheet, identificaciones);

            // Pagos Sheet
            var pagosSheet = package.Workbook.Worksheets.Add("Pagos");
            AddPagosData(pagosSheet, pagos);

            // Confidenciales Sheet
            var confidencialesSheet = package.Workbook.Worksheets.Add("Confidenciales");
            AddConfidencialesData(confidencialesSheet, confidenciales);

            return package.GetAsByteArray();
        }
    }

    private void AddDemograficosData(ExcelWorksheet worksheet, List<Demografico> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Nombre Comercial";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].NombreComercial;
            // Add more columns as needed
        }
    }

    private void AddContributivosData(ExcelWorksheet worksheet, List<Contributivo> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Estatal";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].Estatal;
            // Add more columns as needed
        }
    }

    private void AddAdministrativosData(ExcelWorksheet worksheet, List<Administrativo> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Contrato";
        worksheet.Cells[1, 4].Value = "Facturacion";
        worksheet.Cells[1, 5].Value = "FacturacionBase";
        worksheet.Cells[1, 6].Value = "IVU";
        worksheet.Cells[1, 7].Value = "Staff";
        worksheet.Cells[1, 8].Value = "StaffDate";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].Contrato;
            worksheet.Cells[i + 2, 4].Value = data[i].Facturacion;
            worksheet.Cells[i + 2, 5].Value = data[i].FacturacionBase;
            worksheet.Cells[i + 2, 6].Value = data[i].IVU;
            worksheet.Cells[i + 2, 7].Value = data[i].Staff;
            worksheet.Cells[i + 2, 8].Value = data[i].StaffDate?.ToString("yyyy-MM-dd");
            // Add more columns as needed
        }
    }

    private void AddIdentificacionesData(ExcelWorksheet worksheet, List<Identificacion> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Accionista";
        worksheet.Cells[1, 4].Value = "SSNA";
        worksheet.Cells[1, 5].Value = "Cargo";
        worksheet.Cells[1, 6].Value = "LicConducir";
        worksheet.Cells[1, 7].Value = "Nacimiento";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].Accionista;
            worksheet.Cells[i + 2, 4].Value = data[i].SSNA;
            worksheet.Cells[i + 2, 5].Value = data[i].Cargo;
            worksheet.Cells[i + 2, 6].Value = data[i].LicConducir;
            worksheet.Cells[i + 2, 7].Value = data[i].Nacimiento?.ToString("yyyy-MM-dd");
            // Add more columns as needed
        }
    }

    private void AddPagosData(ExcelWorksheet worksheet, List<Pago> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Banco";
        worksheet.Cells[1, 4].Value = "Tarjeta";
        worksheet.Cells[1, 5].Value = "Expiracion";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].Banco;
            worksheet.Cells[i + 2, 4].Value = data[i].Tarjeta;
            worksheet.Cells[i + 2, 5].Value = data[i].Expiracion?.ToString("yyyy-MM-dd");
            // Add more columns as needed
        }
    }

    private void AddConfidencialesData(ExcelWorksheet worksheet, List<Confidencial> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "UserSuri";
        worksheet.Cells[1, 4].Value = "PassSuri";
        worksheet.Cells[1, 5].Value = "UserEftps";
        worksheet.Cells[1, 6].Value = "PassEftps";
        worksheet.Cells[1, 7].Value = "UserCFSE";
        worksheet.Cells[1, 8].Value = "PassCFSE";
        // Add more headers as needed

        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].UserSuri;
            worksheet.Cells[i + 2, 4].Value = data[i].PassSuri;
            worksheet.Cells[i + 2, 5].Value = data[i].UserEftps;
            worksheet.Cells[i + 2, 6].Value = data[i].PassEftps;
            worksheet.Cells[i + 2, 7].Value = data[i].UserCFSE;
            worksheet.Cells[i + 2, 8].Value = data[i].PassCFSE;
            // Add more columns as needed
        }
    }
}
