using System;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace CPA.Services
{
    public class ExcelImportService
    {
        public DataTable ImportExcel(Stream fileStream)
        {
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var dataTable = new DataTable();

                // Assuming that the first column (A) contains the headers and the second column (B) contains the values
                dataTable.Columns.Add("Field");
                dataTable.Columns.Add("Value");

                for (int rowNum = 1; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var headerCell = worksheet.Cells[rowNum, 1];
                    var valueCell = worksheet.Cells[rowNum, 2];
                    var row = dataTable.NewRow();

                    row["Field"] = headerCell.Text;
                    row["Value"] = valueCell.Text;

                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
        }
    }
}
