using System;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace CPA.Services
{
    public class ExcelImportService
    {
        // Existing method to import from the first sheet
        public DataTable ImportExcel(Stream fileStream)
        {
            return ImportExcel(fileStream, 0); // Default to the first sheet
        }

        // Overloaded method to import from a specific sheet by index
        public DataTable ImportExcel(Stream fileStream, int sheetIndex)
        {
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(fileStream))
            {
                // Ensure the sheet index is within the valid range
                if (sheetIndex < 0 || sheetIndex >= package.Workbook.Worksheets.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(sheetIndex), "Sheet index is out of range.");
                }

                var worksheet = package.Workbook.Worksheets[sheetIndex];
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
