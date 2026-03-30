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
                case "NewMultiTotalReportFormat.html":
                    {
                        AddHeader(workSheet, model.headerList.ToList());
                        StyleHeaderRow(workSheet);

                        int rowNo = 2;

                        foreach (DataRow dr in dt.Rows)
                        {
                            PopulateRow(workSheet, dr, model.colList.ToList(), rowNo);
                            rowNo++;
                        }

                        // Optional: call totals if needed
                        CreateFooterGroupBy(workSheet, dt.AsEnumerable(), rowNo, model.totalFieldList, "Total", true);
                    }
                    break;
                case "CPWiseDetailReport.html":
                    {
                        AddHeader(workSheet, model.headerList.ToList());
                        StyleHeaderRow(workSheet);

                        int rowNo = 2;

                        string prevCP = "";
                        string prevExec = "";
                        string prevBill = "";

                        decimal billGross = 0, billDisc = 0, billNet = 0;
                        decimal billReceipt = 0, billDue = 0, billRefund = 0;

                        decimal subGross = 0, subDisc = 0, subNet = 0;
                        decimal subReceipt = 0, subDue = 0, subRefund = 0;

                        decimal grandGross = 0, grandDisc = 0, grandNet = 0;
                        decimal grandReceipt = 0, grandDue = 0, grandRefund = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            string cp = dr["CompanyOrDoctorName"].ToString();
                            string exec = dr["CompanyOrDoctorNameExecutiveName"].ToString();
                            string bill = dr["PrintBillNo"].ToString();

                            decimal gross = Convert.ToDecimal(dr["ServiceTotalAmt"]);
                            decimal disc = Convert.ToDecimal(dr["ServiceDiscAmt"]);
                            decimal net = Convert.ToDecimal(dr["ServiceNetAmt"]);

                            decimal receipt = Convert.ToDecimal(dr["PaidAmt"]);
                            decimal due = Convert.ToDecimal(dr["BalanceAmt"]);
                            decimal refund = Convert.ToDecimal(dr["RefundAmount"]);

                            /* CP + EXEC CHANGE */
                            if (prevCP != cp || prevExec != exec)
                            {
                                if (prevExec != "")
                                {
                                    WriteTotalRow(workSheet, rowNo++, "Subtotal", subGross, subDisc, subNet, subReceipt, subDue, subRefund);
                                    subGross = subDisc = subNet = subReceipt = subDue = subRefund = 0;
                                }

                                workSheet.Cell(rowNo, 1).Value = $"CPName: {cp} : Executive Name: {exec}";
                                workSheet.Range(rowNo, 1, rowNo, 12).Merge().Style.Font.Bold = true;
                                rowNo++;

                                prevCP = cp;
                                prevExec = exec;
                                prevBill = "";
                            }

                            /* BILL CHANGE */
                            if (prevBill != bill)
                            {
                                if (prevBill != "")
                                {
                                    WriteTotalRow(workSheet, rowNo++, "Bill Total", billGross, billDisc, billNet, billReceipt, billDue, billRefund);
                                    billGross = billDisc = billNet = 0;
                                    billReceipt = billDue = billRefund = 0;
                                }

                                workSheet.Cell(rowNo, 1).Value = bill;
                                workSheet.Cell(rowNo, 2).Value = dr["BillDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["BillDate"]);

                                workSheet.Cell(rowNo, 3).Value = dr["LabRequestNo"]?.ToString();
                                workSheet.Cell(rowNo, 4).Value = dr["PatientName"]?.ToString();
                                workSheet.Cell(rowNo, 5).Value = dr["CompanyOrDoctorName"]?.ToString();
                                rowNo++;

                                billReceipt = receipt;
                                billDue = due;
                                billRefund = refund;

                                subReceipt += receipt;
                                subDue += due;
                                subRefund += refund;

                                grandReceipt += receipt;
                                grandDue += due;
                                grandRefund += refund;

                                prevBill = bill;
                            }

                            /* TEST ROW */
                            workSheet.Cell(rowNo, 1).Value = dr["ServiceName"]?.ToString();
                            workSheet.Cell(rowNo, 6).Value = gross;
                            workSheet.Cell(rowNo, 7).Value = disc;
                            workSheet.Cell(rowNo, 8).Value = net;
                            rowNo++;

                            billGross += gross;
                            billDisc += disc;
                            billNet += net;

                            subGross += gross;
                            subDisc += disc;
                            subNet += net;

                            grandGross += gross;
                            grandDisc += disc;
                            grandNet += net;
                        }

                        /* LAST BILL TOTAL */
                        if (prevBill != "")
                            WriteTotalRow(workSheet, rowNo++, "Bill Total", billGross, billDisc, billNet, billReceipt, billDue, billRefund);

                        /* LAST SUBTOTAL */
                        if (prevExec != "")
                            WriteTotalRow(workSheet, rowNo++, "Subtotal", subGross, subDisc, subNet, subReceipt, subDue, subRefund);

                        /* GRAND TOTAL */
                        WriteTotalRow(workSheet, rowNo++, "GRAND TOTAL", grandGross, grandDisc, grandNet, grandReceipt, grandDue, grandRefund);
                    }
                    break;

                case "DailyCollectionWithSummary.html":
                    {
                        int rowNo = 1;

                        // 🔹 TITLE
                        workSheet.Cell(rowNo, 1).Value = "Daily Collection With Summary";
                        workSheet.Range(rowNo, 1, rowNo, 6).Merge().Style.Font.Bold = true;
                        rowNo += 2;

                        // 🔹 MAIN TABLE HEADER
                        workSheet.Cell(rowNo, 1).Value = "Bill No";
                        workSheet.Cell(rowNo, 2).Value = "Patient Name";
                        workSheet.Cell(rowNo, 3).Value = "Ref Doctor";
                        workSheet.Cell(rowNo, 4).Value = "Mode";
                        workSheet.Cell(rowNo, 5).Value = "Payment";
                        workSheet.Cell(rowNo, 6).Value = "Refund";
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        decimal totalCash = 0, totalNonCash = 0;
                        decimal refundCash = 0, refundNonCash = 0;

                        // 🔹 DETAIL ROWS
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["RowType"].ToString() != "DETAIL") continue;

                            workSheet.Cell(rowNo, 1).Value = dr["PrintBillNo"]?.ToString();
                            workSheet.Cell(rowNo, 2).Value = dr["PatientName"]?.ToString();
                            workSheet.Cell(rowNo, 3).Value = dr["RefDoctor"]?.ToString();
                            workSheet.Cell(rowNo, 4).Value = dr["PayMode"]?.ToString();

                            decimal cash = Convert.ToDecimal(dr["Cash"]);
                            decimal nonCash = Convert.ToDecimal(dr["NonCash"]);

                            workSheet.Cell(rowNo, 5).Value = cash;
                            workSheet.Cell(rowNo, 6).Value = nonCash;

                            rowNo++;
                        }

                        // SPACE
                        rowNo += 2;

                        // 🔹 SUMMARY HEADER
                        workSheet.Cell(rowNo, 1).Value = "Payment Mode";
                        workSheet.Cell(rowNo, 2).Value = "Cash";
                        workSheet.Cell(rowNo, 3).Value = "Non Cash";
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        // 🔹 COLLECTION
                        workSheet.Cell(rowNo, 1).Value = "Collection Summary";
                        workSheet.Range(rowNo, 1, rowNo, 3).Merge().Style.Font.Bold = true;
                        rowNo++;

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["RowType"].ToString() != "SUMMARY") continue;

                            if (dr["Type"].ToString() != "LabBill") continue;

                            string payMode = dr["PayMode"].ToString();
                            decimal cash = Convert.ToDecimal(dr["Cash"]);
                            decimal nonCash = Convert.ToDecimal(dr["NonCash"]);

                            workSheet.Cell(rowNo, 1).Value = payMode;
                            workSheet.Cell(rowNo, 2).Value = cash;
                            workSheet.Cell(rowNo, 3).Value = nonCash;

                            totalCash += cash;
                            totalNonCash += nonCash;

                            rowNo++;
                        }

                        // Total Collection
                        workSheet.Cell(rowNo, 1).Value = "Total Collection";
                        workSheet.Cell(rowNo, 2).Value = totalCash;
                        workSheet.Cell(rowNo, 3).Value = totalNonCash;
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        // 🔹 REFUND
                        rowNo++;
                        workSheet.Cell(rowNo, 1).Value = "Refund Summary";
                        workSheet.Range(rowNo, 1, rowNo, 3).Merge().Style.Font.Bold = true;
                        rowNo++;

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["RowType"].ToString() != "SUMMARY") continue;

                            if (dr["Type"].ToString() == "LabBill") continue;

                            string payMode = dr["PayMode"].ToString();
                            decimal cash = Convert.ToDecimal(dr["Cash"]);
                            decimal nonCash = Convert.ToDecimal(dr["NonCash"]);

                            workSheet.Cell(rowNo, 1).Value = payMode;
                            workSheet.Cell(rowNo, 2).Value = cash;
                            workSheet.Cell(rowNo, 3).Value = nonCash;

                            refundCash += cash;
                            refundNonCash += nonCash;

                            rowNo++;
                        }

                        // Total Refund
                        workSheet.Cell(rowNo, 1).Value = "Total Refund";
                        workSheet.Cell(rowNo, 2).Value = refundCash;
                        workSheet.Cell(rowNo, 3).Value = refundNonCash;
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        // 🔹 NET + GRAND
                        decimal netCash = totalCash - refundCash;
                        decimal netNonCash = totalNonCash - refundNonCash;
                        decimal grandTotal = netCash + netNonCash;

                        rowNo++;
                        workSheet.Cell(rowNo, 1).Value = "Net Collection";
                        workSheet.Cell(rowNo, 2).Value = netCash;
                        workSheet.Cell(rowNo, 3).Value = netNonCash;
                        workSheet.Row(rowNo).Style.Font.Bold = true;

                        rowNo++;
                        workSheet.Cell(rowNo, 1).Value = "Grand Total";
                        workSheet.Cell(rowNo, 2).Value = grandTotal;
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                    }
                    break;
                case "DailyCollectionDetailsWithSummary.html":
                    {
                        if (dt.Rows.Count == 0) break;

                        int rowNo = 1;

                        // 🔹 TITLE
                        workSheet.Cell(rowNo, 1).Value = "Daily Collection Details With Summary";
                        workSheet.Range(rowNo, 1, rowNo, 17).Merge().Style.Font.Bold = true;
                        rowNo += 2;

                        // 🔹 HEADER
                        string[] headers = {
        "Bill Date","Bill No","Lab No","Patient","Tests",
        "Total","Discount","Gross","Net Payable","Paid",
        "Cash Balance","Credit Balance","Refund",
        "Cash","UPI","Card","User"
    };

                        for (int i = 0; i < headers.Length; i++)
                            workSheet.Cell(rowNo, i + 1).Value = headers[i];

                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        // 🔹 DETAIL ROWS
                        foreach (DataRow dr in dt.Select("RowType = 'DETAIL'"))
                        {
                            workSheet.Cell(rowNo, 1).Value = dr["BillDate"]?.ToString();
                            workSheet.Cell(rowNo, 2).Value = dr["PrintBillNo"]?.ToString();
                            workSheet.Cell(rowNo, 3).Value = dr["LabRequestNo"]?.ToString();
                            workSheet.Cell(rowNo, 4).Value = dr["PatientName"]?.ToString();
                            workSheet.Cell(rowNo, 5).Value = dr["TestNames"]?.ToString();

                            workSheet.Cell(rowNo, 6).Value = Convert.ToDecimal(dr["TotalAmt"]);
                            workSheet.Cell(rowNo, 7).Value = Convert.ToDecimal(dr["ConcessionAmt"]);
                            workSheet.Cell(rowNo, 8).Value = Convert.ToDecimal(dr["GrossAmt"]);
                            workSheet.Cell(rowNo, 9).Value = Convert.ToDecimal(dr["NetPayableAmt"]);

                            workSheet.Cell(rowNo, 10).Value = Convert.ToDecimal(dr["TotalMoneyReceived"]);
                            workSheet.Cell(rowNo, 11).Value = Convert.ToDecimal(dr["CashPatientBalance"]);
                            workSheet.Cell(rowNo, 12).Value = Convert.ToDecimal(dr["CreditPatientBalance"]);

                            workSheet.Cell(rowNo, 13).Value = Convert.ToDecimal(dr["LessRefundAmt"]);

                            workSheet.Cell(rowNo, 14).Value = Convert.ToDecimal(dr["NetCashCollection"]);
                            workSheet.Cell(rowNo, 15).Value = Convert.ToDecimal(dr["NetUpiCollection"]);
                            workSheet.Cell(rowNo, 16).Value = Convert.ToDecimal(dr["NetCardCollection"]);

                            workSheet.Cell(rowNo, 17).Value = dr["UserName"]?.ToString();

                            rowNo++;
                        }

                        // 🔹 GET SUMMARY ROWS
                        DataRow summary = dt.Select("RowType = 'SUMMARY'").FirstOrDefault();
                        DataRow payment = dt.Select("RowType = 'PAYMENT SUMMARY'").FirstOrDefault();

                        if (summary == null || payment == null) break;

                        decimal paymentTotal =
                            Convert.ToDecimal(payment["NetCashCollection"] ?? 0) +
                            Convert.ToDecimal(payment["NetUpiCollection"] ?? 0) +
                            Convert.ToDecimal(payment["NetCardCollection"] ?? 0);

                        // 🔹 SPACE
                        rowNo += 2;

                        // 🔹 CASH INDEX (SUMMARY TABLE)
                        workSheet.Cell(rowNo, 10).Value = "Cash Index";
                        workSheet.Range(rowNo, 10, rowNo, 12).Merge().Style.Font.Bold = true;
                        rowNo++;

                        // helper function inline style
                        void WriteSummary(string label, object value)
                        {
                            workSheet.Cell(rowNo, 10).Value = label;
                            workSheet.Cell(rowNo, 12).Value = value?.ToString();
                            rowNo++;
                        }

                        WriteSummary("Total Amount", summary["TotalAmt"]);
                        WriteSummary("Discount Amount", summary["ConcessionAmt"]);
                        WriteSummary("Refund Amount", summary["LessRefundAmt"]);
                        WriteSummary("Gross Amount", summary["GrossAmt"]);
                        WriteSummary("Net Payable", summary["NetPayableAmt"]);

                        WriteSummary("Cash", payment["NetCashCollection"]);
                        WriteSummary("UPI", payment["NetUpiCollection"]);
                        WriteSummary("Card", payment["NetCardCollection"]);

                        // Payment Total
                        workSheet.Cell(rowNo, 10).Value = "Payment Total";
                        workSheet.Cell(rowNo, 12).Value = paymentTotal;
                        workSheet.Row(rowNo).Style.Font.Bold = true;
                        rowNo++;

                        WriteSummary("Total Money Received", summary["TotalMoneyReceived"]);
                        WriteSummary("Cash Balance", summary["CashPatientBalance"]);
                        WriteSummary("Credit Balance", summary["CreditPatientBalance"]);
                    }
                    break;
            }
            return SaveToStream(excel);
        }

        private static void WriteTotalRow(IXLWorksheet ws, int row, string label,
    decimal gross, decimal disc, decimal net,
    decimal receipt, decimal due, decimal refund)
        {
            ws.Cell(row, 1).Value = label;
            ws.Range(row, 1, row, 5).Merge();

            ws.Cell(row, 6).Value = gross;
            ws.Cell(row, 7).Value = disc;
            ws.Cell(row, 8).Value = net;

            ws.Cell(row, 9).Value = receipt;
            ws.Cell(row, 10).Value = due;
            ws.Cell(row, 11).Value = refund;

            ws.Row(row).Style.Font.Bold = true;
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
                            workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
                        }
                        else
                            colspan++;
                    }
                    else if (colName == "lableTotal")
                    {
                        workSheet.Cell(RowNo, 1).Value = $"Total {group}";
                        workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
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
                                workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
                                colspan = 1;
                            }
                            else
                                colspan++;
                        }
                        else if (colName == "lableTotal")
                        {
                            workSheet.Cell(RowNo, col).Value = $"{subGroup} Sub Total";
                            workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
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
                        workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
                    }
                    else
                        colspan++;
                }
                else if (colName == "lableTotal")
                {
                    workSheet.Cell(RowNo, 1).Value = "Grand Total";
                    workSheet.Range(RowNo, 1, RowNo, colspan == 1 ? 1 : (colspan - 1)).Merge();
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
