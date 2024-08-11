using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using DAL.Models;
using OfficeOpenXml.Style;

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
        // Add headers in the first column
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Nombre";
        worksheet.Cells[3, 1].Value = "Nombre Comercial";
        worksheet.Cells[4, 1].Value = "Phone Number";
        worksheet.Cells[5, 1].Value = "Mobile Number";
        worksheet.Cells[6, 1].Value = "Address";
        worksheet.Cells[7, 1].Value = "Business Type";
        worksheet.Cells[8, 1].Value = "Federal Employer Identification Number";
        worksheet.Cells[9, 1].Value = "Social Security Number";
        worksheet.Cells[10, 1].Value = "Date of Incorporation";
        worksheet.Cells[11, 1].Value = "Operations Start Date";
        worksheet.Cells[12, 1].Value = "Industry";
        worksheet.Cells[13, 1].Value = "NAICS Code";
        worksheet.Cells[14, 1].Value = "Description";
        worksheet.Cells[15, 1].Value = "Contact";
        worksheet.Cells[16, 1].Value = "Physical Address";
        worksheet.Cells[17, 1].Value = "Postal Address";
        worksheet.Cells[18, 1].Value = "Email";
        worksheet.Cells[19, 1].Value = "Secondary Email";

        // If there's no data, add an empty row for the structure
        if (data == null || !data.Any())
        {
            worksheet.Cells[1, 2].Value = ""; // Ensure at least one empty cell for the structure
            return;
        }

        // Add data transposed
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].Telefono ?? "";
            worksheet.Cells[5, i + 2].Value = data[i].Celular ?? "";
            worksheet.Cells[6, i + 2].Value = data[i].Dir ?? "";
            worksheet.Cells[7, i + 2].Value = data[i].Tipo ?? "";
            worksheet.Cells[8, i + 2].Value = data[i].Patronal ?? "";
            worksheet.Cells[9, i + 2].Value = data[i].SSN ?? "";
            worksheet.Cells[10, i + 2].Value = data[i].Incorporacion?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cells[11, i + 2].Value = data[i].Operaciones?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cells[12, i + 2].Value = data[i].Industria ?? "";
            worksheet.Cells[13, i + 2].Value = data[i].NAICS;
            worksheet.Cells[13, i + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            worksheet.Cells[14, i + 2].Value = data[i].Descripcion ?? "";
            worksheet.Cells[15, i + 2].Value = data[i].Contacto ?? "";
            worksheet.Cells[16, i + 2].Value = data[i].DirFisica ?? "";
            worksheet.Cells[17, i + 2].Value = data[i].DirPostal ?? "";
            worksheet.Cells[18, i + 2].Value = data[i].Email ?? "";
            worksheet.Cells[19, i + 2].Value = data[i].Email2 ?? "";
        }
    }

    private void AddContributivosData(ExcelWorksheet worksheet, List<Contributivo> data)
    {
        // Add headers
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[1, 2].Value = "Nombre";
        worksheet.Cells[1, 3].Value = "Nombre Comercial";
        worksheet.Cells[1, 4].Value = "State Identification Number";
        worksheet.Cells[1, 5].Value = "CFSE Policy Number";
        worksheet.Cells[1, 6].Value = "Merchant Registry Number";
        worksheet.Cells[1, 7].Value = "Expiration Date";
        worksheet.Cells[1, 8].Value = "Estatal";
        worksheet.Cells[1, 9].Value = "Chauffeur's Insurance Account Number";
        worksheet.Cells[1, 10].Value = "State Department Registry Number";
        // Add data
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 2, 1].Value = data[i].ID;
            worksheet.Cells[i + 2, 2].Value = data[i].Nombre;
            worksheet.Cells[i + 2, 3].Value = data[i].NombreComercial;


            worksheet.Cells[i + 2, 4].Value = data[i].Estatal ?? "";
            worksheet.Cells[i + 2, 5].Value = data[i].Poliza ?? "";
            worksheet.Cells[i + 2, 6].Value = data[i].RegComerciante ?? "";
            worksheet.Cells[i + 2, 7].Value = data[i].Vencimiento?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cells[i + 2, 8].Value = data[i].Choferil ?? "";
            worksheet.Cells[i + 2, 9].Value = data[i].DeptEstado ?? "";

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
