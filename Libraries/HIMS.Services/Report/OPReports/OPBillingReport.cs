using HIMS.Data.DataProviders;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Report.OPReports
{
    public class OPBillingReport : IOPBillingReport
    {

        public OPBillingReport()
        {

        }


        public string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int doctorId, string htmlFilePath, string htmlHeader)
        {

            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = odal.FetchDataTableBySP("rptOPDailyCollectionReport", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NETPayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_DiscAmount = 0, G_NETAmount = 0, G_PaidAmount = 0, G_BalAmount = 0, G_TotAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NETPayAmount = 0, T_NEFTPayAmount = 0, T_PayTmAmount = 0;

            //double G_UserCashPayAmount = 0, G_UserCardPayAmount = 0, G_UserChequePayAmount = 0, G_UserNETPayAmount = 0;
            //double T_UserCashPayAmount = 0, T_UserCardPayAmount = 0, T_UserChequePayAmount = 0, T_UserNETPayAmount = 0;


            double T_BillCash = 0, T_BillCard = 0, T_BillCheque = 0, T_BillNEFT = 0, T_BillPayTm = 0;
            double T_TotAmount = 0, T_DiscAmount = 0, T_NetAmount = 0, T_BalAmount = 0, T_PaidAmount = 0;
            double T_BillReturnCash = 0, T_BillReturnCard = 0, T_BillReturnCheque = 0, T_BillReturnNEFT = 0, T_BillReturnPAYTM = 0;
            double T_BillReturnTot = 0, T_BillReturnDisc = 0, T_BillReturnNet = 0, T_BillReturnPaid = 0, T_BillReturnBal = 0;
            double T_TotalCash = 0, T_TotalCard = 0, T_TotalCheque = 0, T_TotalNEFT = 0, T_TotalPAYTM = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"10\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        // .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_TotAmount = 0; G_DiscAmount = 0; G_NETAmount = 0; G_PaidAmount = 0; G_BalAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"10\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }

                G_TotAmount += dr["TotalAmt"].ConvertToDouble();
                G_DiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                G_NETAmount += dr["NetPayableAmt"].ConvertToDouble();
                G_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                G_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                G_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();

                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();



                //T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                //T_PayTmAmount += dr["PayTMAmount"].ConvertToDouble();

                previousLabel = dr["Type"].ConvertToString();

                items.Append("<tr style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Number"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ReceiptNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {


                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         // .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         //.Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_NETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         //.Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         //.Append(G_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td></tr>");

                }
            }


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;
                if (dr["Type"].ConvertToString() == "OPD Bill")
                {
                    T_TotAmount += dr["TotalAmt"].ConvertToDouble();
                    T_DiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                    T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();
                    T_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                    T_PaidAmount += dr["PaidAmount"].ConvertToDouble();

                    T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    //T_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();
                    T_BillCash += dr["CashPayAmount"].ConvertToDouble();
                    T_BillCard += dr["CardPayAmount"].ConvertToDouble();
                    T_BillCheque += dr["ChequePayAmount"].ConvertToDouble();
                    //T_BillNEFT += dr["NEFTPayAmount"].ConvertToDouble();
                    //T_BillPayTm += dr["PayTMAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "OP Refund of Bill")
                {

                    T_BillReturnTot += dr["TotalAmt"].ConvertToDouble();
                    T_BillReturnDisc += dr["ConcessionAmt"].ConvertToDouble();
                    T_BillReturnNet += dr["NetPayableAmt"].ConvertToDouble();
                    T_BillReturnPaid += dr["PaidAmount"].ConvertToDouble();
                    T_BillReturnBal += dr["BalanceAmt"].ConvertToDouble();
                    T_BillReturnCash += dr["CashPayAmount"].ConvertToDouble();
                    T_BillReturnCard += dr["CardPayAmount"].ConvertToDouble();
                    T_BillReturnCheque += dr["ChequePayAmount"].ConvertToDouble();
                    //T_BillReturnNEFT += dr["NEFTPayAmount"].ConvertToDouble();
                    //T_BillReturnPAYTM += dr["PayTMAmount"].ConvertToDouble();


                }


            }


            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalDisc= T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();

            T_TotalCash = T_BillCash.ConvertToDouble() - T_BillReturnCash.ConvertToDouble();
            T_TotalCard = T_BillCard.ConvertToDouble() - T_BillReturnCard.ConvertToDouble();
            T_TotalCheque = T_BillCheque.ConvertToDouble() - T_BillReturnCheque.ConvertToDouble();
            T_TotalNEFT = T_BillNEFT.ConvertToDouble() - T_BillReturnNEFT.ConvertToDouble();
            T_TotalPAYTM = T_BillPayTm.ConvertToDouble() - T_BillReturnPAYTM.ConvertToDouble();


            TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble() + T_TotalNEFT.ConvertToDouble() + T_TotalPAYTM.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalNEFT}}", T_TotalNEFT.To2DecimalPlace());
            html = html.Replace("{{T_TotalPAYTM}}", T_TotalPAYTM.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{T_BillCash}}", T_BillCash.To2DecimalPlace());
            html = html.Replace("{{T_BillCard}}", T_BillCard.To2DecimalPlace());
            html = html.Replace("{{T_BillCheque}}", T_BillCheque.To2DecimalPlace());
            html = html.Replace("{{T_BillNEFT}}", T_BillNEFT.To2DecimalPlace());
            html = html.Replace("{{T_BillPayTm}}", T_BillPayTm.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnCash}}", T_BillReturnCash.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnCard}}", T_BillReturnCard.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnCheque}}", T_BillReturnCheque.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnNEFT}}", T_BillReturnNEFT.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnPAYTM}}", T_BillReturnPAYTM.To2DecimalPlace());
            html = html.Replace("{{T_TotalCash}}", T_TotalCash.To2DecimalPlace());
            html = html.Replace("{{T_TotalCard}}", T_TotalCard.To2DecimalPlace());
            html = html.Replace("{{T_TotalCheque}}", T_TotalCheque.To2DecimalPlace());
            html = html.Replace("{{T_TotalNEFT}}", T_TotalNEFT.To2DecimalPlace());
            html = html.Replace("{{T_TotalPAYTM}}", T_TotalPAYTM.To2DecimalPlace());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName").ToString());


            return html;
        }



        public string ViewRegistrationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptListofRegistration", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["City"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PinNo"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewDoctorWiseVisitReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDoctorWiseVisitDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            //}

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }


        public string ViewRefDoctorWiseReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("RptRefDoctorWiseAdmission", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["RefDoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["RefDoctorName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }


        public string ViewCrossConsultationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPCrossConsultationReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewOPAppointmentListReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@From_Dt", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@To_Dt", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPAppointmentListReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDepartmentWisecountSummury(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("Rtrv_OPDepartSumry", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["Lbl"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewOPDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("Rtrv_OPDocSumry", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td>");

                T_Count += dr["Lbl"].ConvertToDouble();


            }
            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewOPAppoinmentListWithServiseAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptAppointListWithService", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Medical"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Pathology"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Radiology"].ConvertToDouble()).Append("</td>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewOPDoctorWiseNewOldPatientReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPDocWiseNewOldPatient", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDepartmentWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDrwiseopdcollectiondetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewDayWiseOpdCountDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CityName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");


                T_Count += dr["PatientName"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());






            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDayWiseOpdCountSummry(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_VisitDate = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");

                T_VisitDate += dr["VisitDate"].ConvertToDouble();
            }


            html = html.Replace("{{T_VisitDate}}", T_VisitDate.ToString());







            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }



        public string ViewDepartmentWiseOpdCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            //double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Count"].ConvertToString()).Append("</td>");

                //T_Count += dr["Count"].ConvertToDouble();
            }


            //html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }


        public string ViewDoctorWiseOpdCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            //double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Count"].ConvertToString()).Append("</td>");

                //T_Count += dr["Count"].ConvertToDouble();
            }


            //html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDoctorWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDrwiseopdcollectiondetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_Count += dr["TotalAmt"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;


        }

        public string ViewOPCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPDCollectionReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionReason"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["TotalAmt"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewBillReportSummary(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int16 };
            var Bills = odal.FetchDataTableBySP("rptBillDateWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NeftPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPay"].ConvertToDouble()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewOPDBillBalanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPDCreditBills", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_Count += dr["TotalAmt"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewOPDRefundOfBill(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptOPDRefundOfBill", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_Count += dr["NetPayableAmt"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDepartmentServiceGroupWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDepartmentservicegroupwisecollectionDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewDoctorWiseNewAndOldPatientReport(DateTime fromDate, DateTime toDate, string htmlFilePath, string v)
        {
            throw new NotImplementedException();
        }

        public string ViewOPDailyCollectionReceipt(DateTime fromDate, DateTime toDate, int addedById, string htmlFilePath, string v)
        {
            throw new NotImplementedException();
        }



        public string ViewOPDeptwisecountsummaryReport(DateTime fromDate, DateTime toDate, string htmlFilePath, string v)
        {
            throw new NotImplementedException();
        }

        public string ViewOPDoctorwisecountsummaryReport(DateTime fromDate, DateTime toDate, string htmlFilePath, string v)
        {
            throw new NotImplementedException();
        }


    }
}
