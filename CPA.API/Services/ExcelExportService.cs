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
            var demograficosSheet = package.Workbook.Worksheets.Add("Demographic");
            AddDemograficosData(demograficosSheet, demograficos);

            // Contributivos Sheet
            var contributivosSheet = package.Workbook.Worksheets.Add("Tax Matters");
            AddContributivosData(contributivosSheet, contributivos);

            // Administrativos Sheet
            var administrativosSheet = package.Workbook.Worksheets.Add("Administrative");
            AddAdministrativosData(administrativosSheet, administrativos);

            // Identificaciones Sheet
            var identificacionesSheet = package.Workbook.Worksheets.Add("Identification");
            AddIdentificacionesData(identificacionesSheet, identificaciones);

            // Pagos Sheet
            var pagosSheet = package.Workbook.Worksheets.Add("Payment Methods");
            AddPagosData(pagosSheet, pagos);

            // Confidenciales Sheet
            var confidencialesSheet = package.Workbook.Worksheets.Add("Confidential");
            AddConfidencialesData(confidencialesSheet, confidenciales);

            return package.GetAsByteArray();
        }
    }

    private void AddDemograficosData(ExcelWorksheet worksheet, List<Demografico> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
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
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }

    private void AddContributivosData(ExcelWorksheet worksheet, List<Contributivo> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
        worksheet.Cells[4, 1].Value = "State Identification Number";
        worksheet.Cells[5, 1].Value = "CFSE Policy Number";
        worksheet.Cells[6, 1].Value = "Merchant Registry Number";
        worksheet.Cells[7, 1].Value = "Expiration Date";
        worksheet.Cells[8, 1].Value = "Chauffeur's Insurance Account Number";
        worksheet.Cells[9, 1].Value = "State Department Registry Number";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].Estatal ?? "";
            worksheet.Cells[5, i + 2].Value = data[i].Poliza ?? "";
            worksheet.Cells[6, i + 2].Value = data[i].RegComerciante ?? "";
            worksheet.Cells[7, i + 2].Value = data[i].Vencimiento?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cells[8, i + 2].Value = data[i].Choferil ?? "";
            worksheet.Cells[9, i + 2].Value = data[i].DeptEstado ?? "";
        }
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }

    private void AddAdministrativosData(ExcelWorksheet worksheet, List<Administrativo> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
        worksheet.Cells[4, 1].Value = "Hourly Billing Rate";
        worksheet.Cells[5, 1].Value = "Monthly Billing Rate";
        worksheet.Cells[6, 1].Value = "IVU";
        worksheet.Cells[7, 1].Value = "Staff";
        worksheet.Cells[8, 1].Value = "Staff Assignment Date";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].Contrato;
            worksheet.Cells[5, i + 2].Value = data[i].Facturacion;
            worksheet.Cells[6, i + 2].Value = data[i].FacturacionBase;
            worksheet.Cells[7, i + 2].Value = data[i].IVU;
            worksheet.Cells[8, i + 2].Value = data[i].Staff;
            worksheet.Cells[9, i + 2].Value = data[i].StaffDate?.ToString("yyyy-MM-dd");
        }
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }

    private void AddIdentificacionesData(ExcelWorksheet worksheet, List<Identificacion> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
        worksheet.Cells[4, 1].Value = "Shareholder / Owner";
        worksheet.Cells[5, 1].Value = "Social Security Number";
        worksheet.Cells[6, 1].Value = "Title";
        worksheet.Cells[7, 1].Value = "Driver's License";
        worksheet.Cells[8, 1].Value = "Birth Date";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].Accionista;
            worksheet.Cells[5, i + 2].Value = data[i].SSNA;
            worksheet.Cells[6, i + 2].Value = data[i].Cargo;
            worksheet.Cells[7, i + 2].Value = data[i].LicConducir;
            worksheet.Cells[8, i + 2].Value = data[i].Nacimiento?.ToString("yyyy-MM-dd");
        }
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }

    private void AddPagosData(ExcelWorksheet worksheet, List<Pago> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
        worksheet.Cells[4, 1].Value = "Customer's Name in Bank";
        worksheet.Cells[5, 1].Value = "Bank Account";
        worksheet.Cells[6, 1].Value = "Routing Number";
        worksheet.Cells[7, 1].Value = "Bank's Name";
        worksheet.Cells[8, 1].Value = "Account Type";
        worksheet.Cells[9, 1].Value = "Secondary Customer's Name in Bank";
        worksheet.Cells[10, 1].Value = "Secondary Bank Account";
        worksheet.Cells[11, 1].Value = "Secondary Routing Number";
        worksheet.Cells[12, 1].Value = "Secondary Bank's Name";
        worksheet.Cells[13, 1].Value = "Secondary Account Type";
        worksheet.Cells[14, 1].Value = "Customer's Name in Card";
        worksheet.Cells[15, 1].Value = "Credit Card Number";
        worksheet.Cells[16, 1].Value = "Type of Card";
        worksheet.Cells[17, 1].Value = "CVV";
        worksheet.Cells[18, 1].Value = "Expiration Date";
        worksheet.Cells[19, 1].Value = "Postal Address in Card";
        worksheet.Cells[20, 1].Value = "Secondary Customer's Name in Card";
        worksheet.Cells[21, 1].Value = "Secondary Credit Card Number";
        worksheet.Cells[22, 1].Value = "Secondary Type of Card";
        worksheet.Cells[23, 1].Value = "Secondary CVV";
        worksheet.Cells[24, 1].Value = "Secondary Expiration Date";
        worksheet.Cells[25, 1].Value = "Secondary Postal Address in Card";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].BankClient;
            worksheet.Cells[5, i + 2].Value = data[i].Banco;
            worksheet.Cells[6, i + 2].Value = data[i].NumRuta;
            worksheet.Cells[7, i + 2].Value = data[i].NameBank;
            worksheet.Cells[8, i + 2].Value = data[i].TipoCuenta;
            worksheet.Cells[9, i + 2].Value = data[i].BankClientS;
            worksheet.Cells[10, i + 2].Value = data[i].BancoS;
            worksheet.Cells[11, i + 2].Value = data[i].NumRutaS;
            worksheet.Cells[12, i + 2].Value = data[i].NameBankS;
            worksheet.Cells[13, i + 2].Value = data[i].TipoCuentaS;
            worksheet.Cells[14, i + 2].Value = data[i].NameCard;
            worksheet.Cells[15, i + 2].Value = data[i].Tarjeta;
            worksheet.Cells[16, i + 2].Value = data[i].TipoTarjeta;
            worksheet.Cells[17, i + 2].Value = data[i].CVV;
            worksheet.Cells[18, i + 2].Value = data[i].Expiracion?.ToString("yyyy-MM-dd");
            worksheet.Cells[19, i + 2].Value = data[i].PostalBank;
            worksheet.Cells[20, i + 2].Value = data[i].NameCardS;
            worksheet.Cells[21, i + 2].Value = data[i].TarjetaS;
            worksheet.Cells[22, i + 2].Value = data[i].TipoTarjetaS;
            worksheet.Cells[23, i + 2].Value = data[i].CVVS;
            worksheet.Cells[24, i + 2].Value = data[i].ExpiracionS?.ToString("yyyy-MM-dd");
            worksheet.Cells[25, i + 2].Value = data[i].PostalBankS;
        }
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }

    private void AddConfidencialesData(ExcelWorksheet worksheet, List<Confidencial> data)
    {
        worksheet.Cells[1, 1].Value = "ID";
        worksheet.Cells[2, 1].Value = "Name";
        worksheet.Cells[3, 1].Value = "Commercial Name";
        worksheet.Cells[4, 1].Value = "Username Suri";
        worksheet.Cells[5, 1].Value = "Password Suri";
        worksheet.Cells[6, 1].Value = "Username Eftps";
        worksheet.Cells[7, 1].Value = "PIN";
        worksheet.Cells[8, 1].Value = "Internet Password Eftps";
        worksheet.Cells[9, 1].Value = "Username CFSE";
        worksheet.Cells[10, 1].Value = "Password CFSE";
        worksheet.Cells[11, 1].Value = "Username Department of Labor";
        worksheet.Cells[12, 1].Value = "Password Department of Labor";
        worksheet.Cells[13, 1].Value = "Username Cofim";
        worksheet.Cells[14, 1].Value = "Password Cofim";
        worksheet.Cells[15, 1].Value = "Username Municipality";
        worksheet.Cells[16, 1].Value = "Password Municipality";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[1, i + 2].Value = data[i].ID;
            worksheet.Cells[2, i + 2].Value = data[i].Nombre;
            worksheet.Cells[3, i + 2].Value = data[i].NombreComercial;
            worksheet.Cells[4, i + 2].Value = data[i].UserSuri;
            worksheet.Cells[5, i + 2].Value = data[i].PassSuri;
            worksheet.Cells[6, i + 2].Value = data[i].UserEftps;
            worksheet.Cells[7, i + 2].Value = data[i].PassEftps;
            worksheet.Cells[8, i + 2].Value = data[i].PIN;
            worksheet.Cells[9, i + 2].Value = data[i].UserCFSE;
            worksheet.Cells[10, i + 2].Value = data[i].PassCFSE;
            worksheet.Cells[11, i + 2].Value = data[i].UserDept;
            worksheet.Cells[12, i + 2].Value = data[i].PassDept;
            worksheet.Cells[13, i + 2].Value = data[i].UserCofim;
            worksheet.Cells[14, i + 2].Value = data[i].PassCofim;
            worksheet.Cells[15, i + 2].Value = data[i].UserMunicipio;
            worksheet.Cells[16, i + 2].Value = data[i].PassMunicipio;
        }
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
    }
}
