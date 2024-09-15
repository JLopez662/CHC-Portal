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
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Telefono))
            {
                table.AddCell(new Cell().Add(new Paragraph("Phone Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Telefono)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Celular))
            {
                table.AddCell(new Cell().Add(new Paragraph("Mobile Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Celular)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Dir))
            {
                table.AddCell(new Cell().Add(new Paragraph("Address:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Dir)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Tipo))
            {
                table.AddCell(new Cell().Add(new Paragraph("Business Type:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Tipo)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Patronal))
            {
                table.AddCell(new Cell().Add(new Paragraph("Federal Employer Identification Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Patronal)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.SSN))
            {
                table.AddCell(new Cell().Add(new Paragraph("Social Security Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.SSN)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.Incorporacion.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Date of Incorporation:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Incorporacion.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.Operaciones.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Operations Start Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Operaciones.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Industria))
            {
                table.AddCell(new Cell().Add(new Paragraph("Industry:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Industria)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.NAICS.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("NAICS Code:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NAICS.ToString())).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Descripcion))
            {
                table.AddCell(new Cell().Add(new Paragraph("Description:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Descripcion)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Contacto))
            {
                table.AddCell(new Cell().Add(new Paragraph("Contact:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Contacto)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.DirFisica))
            {
                table.AddCell(new Cell().Add(new Paragraph("Physical Address:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.DirFisica)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.DirPostal))
            {
                table.AddCell(new Cell().Add(new Paragraph("Postal Address:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.DirPostal)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Email))
            {
                table.AddCell(new Cell().Add(new Paragraph("Email:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Email)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Email2))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Email:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Email2)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.CID))
            {
                table.AddCell(new Cell().Add(new Paragraph("CID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.CID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.MID))
            {
                table.AddCell(new Cell().Add(new Paragraph("MID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.MID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }


            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }



    private void AddContributivosSection(Document document, List<Contributivo> data)
    {
        document.Add(new Paragraph("Tax Matters Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        Table table = new Table(2); // Create a table with 2 columns
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Estatal))
            {
                table.AddCell(new Cell().Add(new Paragraph("State Identification Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Estatal)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Poliza))
            {
                table.AddCell(new Cell().Add(new Paragraph("CFSE Policy Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Poliza)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.RegComerciante))
            {
                table.AddCell(new Cell().Add(new Paragraph("Merchant Registry Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.RegComerciante)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.Vencimiento.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Vencimiento.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Choferil))
            {
                table.AddCell(new Cell().Add(new Paragraph("Chauffeur's Insurance Account Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Choferil)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.DeptEstado))
            {
                table.AddCell(new Cell().Add(new Paragraph("State Department Registry Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.DeptEstado)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }


            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }


    private void AddAdministrativosSection(Document document, List<Administrativo> data)
    {
        document.Add(new Paragraph("Administrative Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        Table table = new Table(2); // Create a table with 2 columns
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Contrato))
            {
                table.AddCell(new Cell().Add(new Paragraph("Hourly Billing Rate:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Contrato)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Facturacion))
            {
                table.AddCell(new Cell().Add(new Paragraph("Monthly Billing Rate:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Facturacion)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }


            if (!string.IsNullOrEmpty(item.IVU))
            {
                table.AddCell(new Cell().Add(new Paragraph("IVU:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.IVU)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Staff))
            {
                table.AddCell(new Cell().Add(new Paragraph("Staff:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Staff)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.StaffDate.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Staff Assignment Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.StaffDate.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }


    private void AddIdentificacionesSection(Document document, List<Identificacion> data)
    {
        document.Add(new Paragraph("Identification Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        Table table = new Table(2); // Create a table with 2 columns
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Accionista))
            {
                table.AddCell(new Cell().Add(new Paragraph("Shareholder / Owner:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Accionista)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.SSNA))
            {
                table.AddCell(new Cell().Add(new Paragraph("Social Security Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.SSNA)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Cargo))
            {
                table.AddCell(new Cell().Add(new Paragraph("Title:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Cargo)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.LicConducir))
            {
                table.AddCell(new Cell().Add(new Paragraph("Driver's License:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.LicConducir)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.Nacimiento.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Birth Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nacimiento.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }


    private void AddPagosSection(Document document, List<Pago> data)
    {
        document.Add(new Paragraph("Payment Methods")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        Table table = new Table(2); // Create a table with 2 columns
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.BankClient))
            {
                table.AddCell(new Cell().Add(new Paragraph("Customer's Name in Bank:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.BankClient)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Banco))
            {
                table.AddCell(new Cell().Add(new Paragraph("Bank Account:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Banco)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NumRuta))
            {
                table.AddCell(new Cell().Add(new Paragraph("Routing Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NumRuta)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NameBank))
            {
                table.AddCell(new Cell().Add(new Paragraph("Bank's Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NameBank)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.TipoCuenta))
            {
                table.AddCell(new Cell().Add(new Paragraph("Account Type:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.TipoCuenta)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.BankClientS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Customer's Name in Bank:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.BankClientS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT)); ;
            }

            if (!string.IsNullOrEmpty(item.BancoS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Bank Account:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.BancoS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NumRutaS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Routing Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NumRutaS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NameBankS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Bank's Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NameBankS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.TipoCuentaS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Account Type:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.TipoCuentaS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NameCard))
            {
                table.AddCell(new Cell().Add(new Paragraph("Customer's Name in Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NameCard)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Tarjeta))
            {
                table.AddCell(new Cell().Add(new Paragraph("Credit Card Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Tarjeta)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.TipoTarjeta))
            {
                table.AddCell(new Cell().Add(new Paragraph("Type of Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.TipoTarjeta)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.CVV))
            {
                table.AddCell(new Cell().Add(new Paragraph("CVV:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.CVV)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.Expiracion.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Expiracion.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PostalBank))
            {
                table.AddCell(new Cell().Add(new Paragraph("Postal Address in Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PostalBank)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NameCardS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Customer's Name in Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NameCardS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.TarjetaS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Credit Card Number:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.TarjetaS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.TipoTarjetaS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Type of Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.TipoTarjetaS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.CVVS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary CVV:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.CVVS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (item.ExpiracionS.HasValue)
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Expiration Date:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ExpiracionS.Value.ToString("yyyy-MM-dd"))).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PostalBankS))
            {
                table.AddCell(new Cell().Add(new Paragraph("Secondary Postal Address in Card:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PostalBankS)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }


    private void AddConfidencialesSection(Document document, List<Confidencial> data)
    {
        document.Add(new Paragraph("Confidential Information")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16)
            .SetBold()
            .SetMarginBottom(20));

        Table table = new Table(2); // Create a table with 2 columns
        table.SetWidth(UnitValue.CreatePercentValue(100));

        foreach (var item in data)
        {
            // Add fields conditionally only if they are not null or empty
            if (!string.IsNullOrEmpty(item.ID))
            {
                table.AddCell(new Cell().Add(new Paragraph("ID:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.ID)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.Nombre))
            {
                table.AddCell(new Cell().Add(new Paragraph("Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.Nombre)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.NombreComercial))
            {
                table.AddCell(new Cell().Add(new Paragraph("Commercial Name:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.NombreComercial)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserSuri))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username Suri:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserSuri)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassSuri))
            {
                table.AddCell(new Cell().Add(new Paragraph("Password Suri:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassSuri)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserEftps))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username Eftps:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserEftps)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PIN))
            {
                table.AddCell(new Cell().Add(new Paragraph("PIN:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PIN)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassEftps))
            {
                table.AddCell(new Cell().Add(new Paragraph("Internet Password Eftps:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassEftps)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserCFSE))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username CFSE:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserCFSE)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassCFSE))
            {
                table.AddCell(new Cell().Add(new Paragraph("Password CFSE:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassCFSE)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserDept))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username Department of Labor:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserDept)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassDept))
            {
                table.AddCell(new Cell().Add(new Paragraph("Password Department of Labor:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassDept)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserCofim))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username Cofim:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserCofim)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassCofim))
            {
                table.AddCell(new Cell().Add(new Paragraph("Password Cofim:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassCofim)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.UserMunicipio))
            {
                table.AddCell(new Cell().Add(new Paragraph("Username Municipality:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.UserMunicipio)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            if (!string.IsNullOrEmpty(item.PassMunicipio))
            {
                table.AddCell(new Cell().Add(new Paragraph("Password Municipality:").SetBold()).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph(item.PassMunicipio)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
            }

            // Add an empty new line before the separator
            table.AddCell(new Cell(1, 2).Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));

            // Add a separator line between each record
            Cell separatorCell = new Cell(1, 2).Add(new LineSeparator(new SolidLine())).SetBorder(Border.NO_BORDER);
            table.AddCell(separatorCell);
        }

        // Add the table to the document
        document.Add(table);

        // Add an empty new line after the table
        document.Add(new Paragraph("\n"));
    }



}
