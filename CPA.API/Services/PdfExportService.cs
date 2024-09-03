using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;
using DAL.Models;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;

public class PdfExportService
{
    public byte[] ExportToPdf(
        List<Demografico> demograficos,
        List<Contributivo> contributivos,
        List<Administrativo> administrativos,
        List<Identificacion> identificaciones,
        List<Pago> pagos,
        List<Confidencial> confidenciales)
    {
        using (var stream = new MemoryStream())
        {
            var pdfWriter = new PdfWriter(stream);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            // Add sections to the PDF
            AddDemograficosSection(document, demograficos);
            AddContributivosSection(document, contributivos);
            AddAdministrativosSection(document, administrativos);
            AddIdentificacionesSection(document, identificaciones);
            AddPagosSection(document, pagos);
            AddConfidencialesSection(document, confidenciales);

            document.Close();

            return stream.ToArray();
        }
    }

    private void AddDemograficosSection(Document document, List<Demografico> data)
    {
        // Centered title
        document.Add(new Paragraph("Demographic Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        // Create a table with two columns
        Table table = new Table(2);
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add the labels and values as table cells without borders
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Phone Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Telefono)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Mobile Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Celular)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Address:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Dir)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Business Type:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Tipo)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Federal Employer Identification Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Patronal)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Social Security Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.SSN)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Date of Incorporation:").SetBold()).SetBorder(Border.NO_BORDER));
            string incorporationDate = item.Incorporacion.HasValue ? item.Incorporacion.Value.ToString("yyyy-MM-dd") : "N/A";
            table.AddCell(new Cell().Add(new Paragraph(incorporationDate)).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);
    }


    private void AddContributivosSection(Document document, List<Contributivo> data)
    {
        document.Add(new Paragraph("Tax Matters Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold());

        Table table = new Table(2); // Create a table with 2 columns

        foreach (var item in data)
        {
            // Add the labels and values as table cells, replacing null values with "N/A"
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("State Identification Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Estatal ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("CFSE Policy Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Poliza ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Merchant Registry Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.RegComerciante ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Vencimiento?.ToString("yyyy-MM-dd") ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Chauffeur's Insurance Account Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Choferil ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("State Department Registry Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.DeptEstado ?? "N/A")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);
    }



    private void AddAdministrativosSection(Document document, List<Administrativo> data)
    {
        document.Add(new Paragraph("Administrative Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold());

        Table table = new Table(2); // Create a table with 2 columns

        foreach (var item in data)
        {
            // Add the labels and values as table cells, replacing null values with "N/A"
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Hourly Billing Rate:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Contrato?.ToString() ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Monthly Billing Rate:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Facturacion?.ToString() ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("IVU:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.IVU ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Staff:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Staff ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Staff Assignment Date:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.StaffDate?.ToString("yyyy-MM-dd") ?? "N/A")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);

        }

        // Add the table to the document
        document.Add(table);
    }


    private void AddIdentificacionesSection(Document document, List<Identificacion> data)
    {

        // Add a page break before the table section
        document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

        document.Add(new Paragraph("Identification Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold());

        Table table = new Table(2); // Create a table with 2 columns

        foreach (var item in data)
        {
            // Add the labels and values as table cells without borders
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Shareholder / Owner:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Accionista ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Social Security Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.SSNA ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Title:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Cargo ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Driver's License:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.LicConducir ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Birth Date:").SetBold()).SetBorder(Border.NO_BORDER));
            string birthDate = item.Nacimiento.HasValue ? item.Nacimiento.Value.ToString("yyyy-MM-dd") : "N/A";
            table.AddCell(new Cell().Add(new Paragraph(birthDate)).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);
    }


    private void AddPagosSection(Document document, List<Pago> data)
    {
        document.Add(new Paragraph("Payment Methods")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold());

        Table table = new Table(2); // Create a table with 2 columns

        foreach (var item in data)
        {
            // Add the labels and values as table cells without borders
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Customer's Name in Bank:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.BankClient ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Bank Account:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Banco ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Routing Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NumRuta ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Bank's Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NameBank ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Account Type:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.TipoCuenta ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Customer's Name in Bank:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.BankClientS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Bank Account:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.BancoS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Routing Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NumRutaS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Bank's Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NameBankS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Account Type:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.TipoCuentaS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Customer's Name in Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NameCard ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Credit Card Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Tarjeta ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Type of Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.TipoTarjeta ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("CVV:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.CVV ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
            string expirationDate = item.Expiracion.HasValue ? item.Expiracion.Value.ToString("yyyy-MM-dd") : "N/A";
            table.AddCell(new Cell().Add(new Paragraph(expirationDate)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Postal Address in Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PostalBank ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Customer's Name in Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NameCardS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Credit Card Number:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.TarjetaS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Type of Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.TipoTarjetaS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary CVV:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.CVVS ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
            string secondaryExpirationDate = item.ExpiracionS.HasValue ? item.ExpiracionS.Value.ToString("yyyy-MM-dd") : "N/A";
            table.AddCell(new Cell().Add(new Paragraph(secondaryExpirationDate)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Secondary Postal Address in Card:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PostalBankS ?? "N/A")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);
    }

    private void AddConfidencialesSection(Document document, List<Confidencial> data)
    {
        document.Add(new Paragraph("Confidential Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold());

        Table table = new Table(2); // Create a table with 2 columns

        foreach (var item in data)
        {
            // Add the labels and values as table cells without borders
            table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.ID ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.Nombre ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username Suri:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserSuri ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Password Suri:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassSuri ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username Eftps:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserEftps ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("PIN:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PIN ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Internet Password Eftps:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassEftps ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username CFSE:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserCFSE ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Password CFSE:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassCFSE ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username Department of Labor:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserDept ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Password Department of Labor:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassDept ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username Cofim:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserCofim ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Password Cofim:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassCofim ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Username Municipality:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.UserMunicipio ?? "N/A")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Password Municipality:").SetBold()).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph(item.PassMunicipio ?? "N/A")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);
    }


}
