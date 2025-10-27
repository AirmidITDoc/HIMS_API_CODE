using ClosedXML.Excel;
using HIMS.Core.Domain.Grid;
using System.Data;
using System.Text;

namespace HIMS.API.Extensions
{
    public static class ReportExcelHelper
    {
        private static void AddHeader(IXLWorksheet workSheet, List<string> columns)
        {
            int colIndex = 1;
            foreach (var column in columns.Where(x => x.Replace(".", "").ToLower() != "srno"))
            {
                workSheet.Cell(1, colIndex++).Value = column;
            }
        }
        private static void StyleHeaderRow(IXLWorksheet workSheet)
        {
            var headerRow = workSheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Height = 30;
        }
        private static void PopulateRow(IXLWorksheet workSheet, DataRow item, List<string> columns, int rowIndex)
        {
            int colIndex = 1;
            // for Sr No
            //workSheet.Cell(rowIndex, colIndex).Value = rowIndex - 1;

            foreach (var column in columns.Where(x => x.Replace(".", "").ToLower() != "srno"))
            {
                workSheet.Cell(rowIndex, colIndex).Value = item[column].ToString();
                colIndex++;
            }
        }
        public static Stream SaveToStream(XLWorkbook excel)
        {
            var memoryStream = new MemoryStream();
            excel.SaveAs(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
        public static Stream GetExcel(ReportConfigDto model, DataTable dt)
        {
            using var excel = new XLWorkbook();
            var workSheet = excel.Worksheets.Add("sheetName");
            switch (model.htmlFilePath)
            {
                // Simple Report Format
                case "SimpleReportFormat.html":
                    {
                        // Create and style the header row
                        AddHeader(workSheet, model.headerList.ToList());
                        StyleHeaderRow(workSheet);
                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;
                            PopulateRow(workSheet, dr, model.colList.ToList(), k + 1);
                        }
                    }
                    break;
                case "MultiTotalReportFormat.html":
                    {
                        // Create and style the header row
                        AddHeader(workSheet, model.headerList.ToList());
                        StyleHeaderRow(workSheet);
                        GetMultiTableExcel(workSheet, dt, model);
                    }
                    break;
            }
            return SaveToStream(excel);
        }
        public static void GetMultiTableExcel(IXLWorksheet workSheet, DataTable dt, ReportConfigDto model)
        {
            StringBuilder table = new();
            string[] groupBy = model.groupByLabel.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            int RowNo = 2;
            if (groupBy.Length > 0)
            {
                var groups1 = dt.AsEnumerable().Select(row => row.Field<string>(groupBy[0])).Distinct().ToList();
                foreach (string group1 in groups1)
                {
                    var group1Data = dt.Select(groupBy[0] + "='" + group1 + "'");
                    workSheet.Cell(RowNo, 1).Value = group1;
                    workSheet.Row(RowNo).Style.Font.Bold = true;
                    workSheet.Range(RowNo, 1, RowNo, model.headerList.Length).Merge();
                    RowNo++;
                    if (groupBy.Length > 1)
                    {
                        var groups2 = group1Data.AsEnumerable().Select(row => row.Field<string>(groupBy[1])).Distinct().ToList();
                        foreach (string group2 in groups2)
                        {
                            var group2Data = group1Data.Where(x => x[groupBy[1]].ToString().ToLower() == group2.ToLower());
                            workSheet.Cell(RowNo, 1).Value = group2;
                            workSheet.Row(RowNo).Style.Font.Bold = true;
                            workSheet.Range(RowNo, 1, RowNo, model.headerList.Length).Merge();
                            RowNo++;
                            if (groupBy.Length > 2)
                            {
                                var groups3 = group2Data.AsEnumerable().Select(row => row.Field<string>(groupBy[2])).Distinct().ToList();
                                foreach (string group3 in groups3)
                                {
                                    var group3Data = group2Data.Where(x => x[groupBy[2]].ToString().ToLower() == group3.ToLower());
                                    workSheet.Cell(RowNo, 1).Value = group3;
                                    workSheet.Row(RowNo).Style.Font.Bold = true;
                                    workSheet.Range(RowNo, 1, RowNo, model.headerList.Length).Merge();
                                    RowNo++;
                                    if (groupBy.Length > 3)
                                    {
                                        var groups4 = group3Data.AsEnumerable().Select(row => row.Field<string>(groupBy[3])).Distinct().ToList();
                                        foreach (string group4 in groups4)
                                        {

                                            workSheet.Cell(RowNo, 1).Value = group4;
                                            workSheet.Row(RowNo).Style.Font.Bold = true;
                                            workSheet.Range(RowNo, 1, RowNo, model.headerList.Length).Merge();
                                            RowNo++;
                                            foreach (var row in group2Data)
                                            {
                                                PopulateRow(workSheet, row, model.colList.ToList(), RowNo);
                                                RowNo++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var row in group3Data)
                                        {
                                            PopulateRow(workSheet, row, model.colList.ToList(), RowNo);
                                            RowNo++;
                                        }
                                    }
                                    CreateFooterGroupBy(workSheet, group3Data, RowNo, model.totalFieldList, group3);
                                    RowNo++;
                                }
                            }
                            else
                            {
                                foreach (var row in group2Data)
                                {
                                    PopulateRow(workSheet, row, model.colList.ToList(), RowNo);
                                    RowNo++;
                                }
                            }
                            if (model.groupByLabel.Split(',').Where(x => x != "").ToArray().Any(x => !string.IsNullOrWhiteSpace(x)))
                            {
                                CreateFooterGroupBy(workSheet, group2Data, RowNo, model.totalFieldList, group2);
                                RowNo++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var row in group1Data)
                        {
                            PopulateRow(workSheet, row, model.colList.ToList(), RowNo);
                            RowNo++;
                        }
                    }
                    if (model.groupByLabel.Split(',').Where(x => x != "").ToArray().Any(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        CreateFooterGroupBy(workSheet, group1Data, RowNo, model.totalFieldList, group1);
                        RowNo++;
                    }
                }
                if (model.groupByLabel.Split(',').Where(x => x != "").ToArray().Any(x => !string.IsNullOrWhiteSpace(x)))
                {
                    CreateFooterGroupBy(workSheet, dt.AsEnumerable(), RowNo, model.totalFieldList, "Total", true);
                    RowNo++;
                }
            }
            else
            {
                foreach (var row in dt.AsEnumerable())
                {
                    PopulateRow(workSheet, row, model.colList.ToList(), RowNo);
                    RowNo++;
                }
            }
            if (model.Mode == "DailyCollectionSummary")
                CreateSummaryIncome(workSheet, dt, RowNo, model.headerList, model.groupByLabel.Split(',').Where(x => x != "").ToArray(), model.totalFieldList);
        }
        public static void CreateFooterGroupBy(IXLWorksheet workSheet, IEnumerable<DataRow> groupData, int RowNo, string[] footer, string groupName, bool isTotal = false)
        {
            if (!footer.Any(x => !string.IsNullOrWhiteSpace(x)))
                return;
            int colspan = 0;
            int col = 1;
            // for Sr No
            //workSheet.Cell(RowNo, colIndex).Value = "";

            foreach (var hr in footer.Skip(1))
            {
                string total = "";
                if (hr.ToLower() == "space")
                    colspan++;
                else if (hr.ToLower() == "labletotal")
                    total = isTotal ? "Total" : ("Sub Total for " + groupName);
                else
                    total = groupData.Sum(row => row.IsNull(hr) ? 0 : Convert.ToDecimal(row[hr])).ToString();
                if (!string.IsNullOrWhiteSpace(total) || footer.Length == col)
                {
                    workSheet.Cell(RowNo, col).Value = total;
                    if (colspan > 0)
                        workSheet.Range(RowNo, col, RowNo, col + colspan).Merge();
                    col += colspan + 1;
                    colspan = 0;
                }
            }
            workSheet.Row(RowNo).Style.Font.Bold = true;
        }

        public static void CreateSummaryIncome(IXLWorksheet workSheet, DataTable dt, int RowNo, string[] headers, string[] groupCol, string[] totalColList)
        {
            // Define expected groups
            string[] groupNames = { "Income", "Expense" };

            // Dictionary to store totals for each group
            Dictionary<string, Dictionary<string, decimal>> groupTotals = new();
            workSheet.Cell(RowNo, 1).Value = "Summary";
            workSheet.Range(RowNo, 1, RowNo, headers.Length + 1).Merge();
            workSheet.Row(RowNo).Style.Font.FontSize = 20;
            int col = 1; int colspan = 1;
            foreach (var group in groupNames)
            {
                RowNo++;
                groupTotals[group] = new Dictionary<string, decimal>();

                // Get distinct subgroups for this main group
                var subGroups = dt.AsEnumerable()
                                  .Where(row => row[groupCol[0]].ToString() == group)
                                  .Select(row => row[groupCol[1]].ToString())
                                  .Distinct();


                // Group Total row
                col = 1; colspan = 1;
                foreach (var colName in totalColList)
                {
                    if (colName == "space")
                    {
                        if (totalColList.Length == col)
                        {
                            workSheet.Cell(RowNo, 1).Value = "Summary";
                            workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                        }
                        else
                            colspan++;
                    }
                    else if (colName == "lableTotal")
                    {
                        workSheet.Cell(RowNo, 1).Value = $"Total {group}";
                        workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                        colspan = 1;
                    }
                    else
                    {
                        decimal total = dt.Select($"{groupCol[0]} = '{group}'")
                                          .Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName]));
                        groupTotals[group][colName] = total;

                        workSheet.Cell(RowNo, col - 1).Value = total;
                    }
                    col++;
                }
                workSheet.Row(RowNo).Style.Font.Bold = true;

                // Start new table for each group
                foreach (var subGroup in subGroups)
                {
                    RowNo++;
                    col = 1; colspan = 1;
                    foreach (var colName in totalColList)
                    {
                        if (colName == "space")
                        {
                            if (totalColList.Length == col)
                            {
                                workSheet.Cell(RowNo, 1).Value = "";
                                workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                                colspan = 1;
                            }
                            else
                                colspan++;
                        }
                        else if (colName == "lableTotal")
                        {
                            workSheet.Cell(RowNo, col).Value = $"{subGroup} Sub Total";
                            workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                            colspan = 1;
                        }
                        else
                        {
                            decimal subTotal = dt.Select($"{groupCol[0]} = '{group}' AND {groupCol[1]} = '{subGroup}'")
                                                 .Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName]));
                            workSheet.Cell(RowNo, col - 1).Value = subTotal;
                        }
                        col++;
                    }
                }
            }

            // Grand Total row (Income - Expense)
            col = 1; colspan = 1;
            RowNo++;
            foreach (var colName in totalColList)
            {
                if (colName == "space")
                {
                    if (totalColList.Length == col)
                    {
                        workSheet.Cell(RowNo, 1).Value = "";
                        workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                    }
                    else
                        colspan++;
                }
                else if (colName == "lableTotal")
                {
                    workSheet.Cell(RowNo, 1).Value = "Grand Total";
                    workSheet.Range(RowNo, 1, RowNo, colspan - 1).Merge();
                    colspan = 1;
                }
                else
                {
                    decimal income = groupTotals.ContainsKey("Income") && groupTotals["Income"].ContainsKey(colName)
                        ? groupTotals["Income"][colName] : 0;
                    decimal expense = groupTotals.ContainsKey("Expense") && groupTotals["Expense"].ContainsKey(colName)
                        ? groupTotals["Expense"][colName] : 0;
                    decimal net = income - expense;
                    workSheet.Cell(RowNo, col - 1).Value = net;
                }
                col++;
            }
            workSheet.Row(RowNo).Style.Font.Bold = true;
        }

    }
}
