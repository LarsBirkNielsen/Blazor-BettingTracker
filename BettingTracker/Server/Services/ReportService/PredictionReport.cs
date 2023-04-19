
using OfficeOpenXml;

namespace BettingTracker.Server.Services.ReportService
{
    public class PredictionReport
    {
        public static ExcelPackage CreateReport(IWebHostEnvironment webHostEnvironment)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Employee Report";
            package.Workbook.Properties.Author = "BLC";
            package.Workbook.Properties.Subject = "Employee List";
            package.Workbook.Properties.Keywords = "Employee List Report";

            var workSheet = package.Workbook.Worksheets.Add("Employee");

            workSheet.Cells[1, 1].Value = "Employee ID";
            workSheet.Cells[1, 2].Value = "Name";
            workSheet.Cells[1, 3].Value = "Gender";
            workSheet.Cells[1, 4].Value = "Salary";

            var numFormat = "#,##0";
            var dataCellStyle = "TableName";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyle);
            numStyle.Style.Numberformat.Format = numFormat;

            workSheet.Cells[2, 1].Value = 2;
            workSheet.Cells[2, 2].Value = "John";
            workSheet.Cells[2, 3].Value = "Male";
            workSheet.Cells[2, 4].Value = "40000";
            workSheet.Cells[2, 4].Style.Numberformat.Format = numFormat;

            workSheet.Cells[3, 1].Value = 2;
            workSheet.Cells[3, 2].Value = "John";
            workSheet.Cells[3, 3].Value = "Male";
            workSheet.Cells[3, 4].Value = "40000";
            workSheet.Cells[3, 4].Style.Numberformat.Format = numFormat;

            workSheet.Cells[4, 1].Value = 2;
            workSheet.Cells[4, 2].Value = "John";
            workSheet.Cells[4, 3].Value = "Male";
            workSheet.Cells[4, 4].Value = "40000";
            workSheet.Cells[4, 4].Style.Numberformat.Format = numFormat;

            var table = workSheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: 4, toColumn: 4), "data");
            table.ShowHeader = true;
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Dark3;
            table.ShowTotal = true;
            table.Columns[3].DataCellStyleName = dataCellStyle;
            table.Columns[3].TotalsRowFunction = OfficeOpenXml.Table.RowFunctions.Sum;

            workSheet.Cells[1, 1, 4, 4].AutoFitColumns();

            return package;


        }

    }
}
