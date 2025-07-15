using HIMS.Data.DataProviders;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace HIMS.Services.Report.OPReports
{
    public class OPBillingReport : IOPBillingReport
    {

        public OPBillingReport()
        {

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

            StringBuilder items = new("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["City"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PinNo"].ConvertToString()).Append("</td>");
             
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount=0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;
                 
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                //T_Count += dr["PatientName"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       
                         .Append(Dcount).Append("</td></tr>");


                }

            }
            
            html = html.Replace("{{T_Count}}", T_Count.ToString());
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
            var Bills = odal.FetchDataTableBySP("rptReferenceDoctor", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["RefDoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;

                previousLabel = dr["RefDoctorName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");



                }

            }
            html = html.Replace("{{T_Count}}", T_Count.ToString());
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

            StringBuilder items = new("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align:center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

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

            StringBuilder items = new("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;width:10%;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;width:7%;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;width:10%;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

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

            StringBuilder items = new("");
            int i = 0, j = 0;
             double   T_Count=0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td></tr>");

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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td></tr>");

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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Medical = 0, T_Pathology = 0, T_Radiology = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Medical"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Pathology"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Radiology"].ConvertToDouble()).Append("</td></tr>");

                T_Medical += dr["Medical"].ConvertToDouble();

                T_Pathology += dr["Pathology"].ConvertToDouble();
                T_Radiology += dr["Radiology"].ConvertToDouble();
            }


            //html = html.Replace("{{T_Medical}}", Bills.GetColValue("T_Medical").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{T_Medical}}", T_Medical.ToString());


            html = html.Replace("{{T_Pathology}}", T_Pathology.ToString());
            html = html.Replace("{{T_Radiology}}", T_Radiology.ToString());

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
            var Bills = odal.FetchDataTableBySP("rptOPDocWiseNewOldPatient_web", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["PatientOldAndNew"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["PatientOldAndNew"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientOldAndNew"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;

                previousLabel = dr["PatientOldAndNew"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td></tr>");



                //T_Count += dr["PatientName"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());

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

            StringBuilder items = new("");
            int i = 0;
            double T_TotalAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();

            }
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToDateString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToDateString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;
                previousLabel = dr["DoctorName"].ConvertToDateString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CityName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }
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
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_COUNT = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitCount"].ConvertToString()).Append("</td></tr>");

                T_COUNT += dr["VisitCount"].ConvertToDouble();
            }


            html = html.Replace("{{T_COUNT}}", T_COUNT.ToString());







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
            var Bills = odal.FetchDataTableBySP("rptDepartmentDaywiseopdcountSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitCount"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["VisitCount"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



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
            var Bills = odal.FetchDataTableBySP("rptDaywiseDoctoropdcountSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitCount"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["VisitCount"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



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
            var Bills = odal.FetchDataTableBySP("rptDrwiseopdcollectionSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["VisitDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["VisitDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["VisitDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;
                previousLabel = dr["VisitDate"].ConvertToDateString("dd/MM/yyyy");


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append(" </td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Amount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }
                T_Count += dr["Amount"].ConvertToDouble();
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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_RefundAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionReason"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
               
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                T_RefundAmount += dr["RefundAmount"].ConvertToDouble();
            }


            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.ToString());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.ToString());
            html = html.Replace("{{T_NEFTPayAmount}}", T_NEFTPayAmount.ToString());
            html = html.Replace("{{T_PayTMAmount}}", T_PayTMAmount.ToString());
            html = html.Replace("{{T_RefundAmount}}", T_RefundAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewBillReportSummary(DateTime FromDate, DateTime ToDate,int AddedById, string htmlFilePath, string htmlHeader)
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

            StringBuilder items = new("");
            int i = 0;
            double T_BillAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_PaidAmount = 0, T_BalanceAmt = 0, T_CashPay = 0, T_ChequePay = 0, T_CardPay = 0, T_NeftPay = 0, T_PayTMPay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                
                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NeftPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPay"].ConvertToDouble()).Append("</td></tr>");

                T_BillAmt += dr["BillAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
                T_CashPay += dr["CashPay"].ConvertToDouble();
                T_ChequePay += dr["ChequePay"].ConvertToDouble();
                T_CardPay += dr["CardPay"].ConvertToDouble();
                T_NeftPay += dr["NeftPay"].ConvertToDouble();
                T_PayTMPay += dr["PayTMPay"].ConvertToDouble();
            }


            html = html.Replace("{{T_BillAmt}}", T_BillAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
            html = html.Replace("{{T_CashPay}}", T_CashPay.ToString());
            html = html.Replace("{{T_ChequePay}}", T_ChequePay.ToString());
            html = html.Replace("{{T_CardPay}}", T_CardPay.ToString());
            html = html.Replace("{{T_NeftPay}}", T_NeftPay.ToString());
            html = html.Replace("{{T_PayTMPay}}", T_PayTMPay.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewBillReportSummarySummary(DateTime FromDate, DateTime ToDate,  string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();


            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
         
            var Bills = odal.FetchDataTableBySP("rptBillDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0;
            double T_BillAmt = 0, T_ConcessionAmt = 0, T_PaidAmount = 0, T_BalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");
               

                T_BillAmt += dr["BillAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
         
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
               
            }


            html = html.Replace("{{T_BillAmt}}", T_BillAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
        
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
            

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

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_NetPayableAmt = 0, T_PaidAmount = 0, T_BalanceAmt = 0, T_TotalAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
            }


            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
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

            StringBuilder items = new("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                
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
            var Bills = odal.FetchDataTableBySP("rptservicegroupwisecollectionSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;

                previousLabel = dr["DepartmentName"].ConvertToString();
                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }
                T_Count += dr["TotalAmount"].ConvertToDouble();
            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        //public string ViewDoctorWiseNewAndOldPatientReport(DateTime fromDate, DateTime toDate, string htmlFilePath, string htmlHeader)
        //{
        //    SqlParameter[] para = new SqlParameter[2];
        //    para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    var Bills = GetDataTableProc("rptOPDocWiseNewOldPatient", para);


        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{HospitalHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;

        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;

        //        items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td></tr>");


        //    }

        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;
        //}

       
        

       

        public string ViewDepartmentWiseOPDCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;

                previousLabel = dr["DepartmentName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CityName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }
            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDrWiseOPDCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDaywiseopdcountdetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CityName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");



                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Day Wise Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }

            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDrWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDrwiseopdcollectiondetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, T_TotalAmt = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;'><td colspan='12' style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Doctor Wise Collection</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(T_TotalAmt).Append("</td></tr>");

                    T_TotalAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"12\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
             
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='12' style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Doctor Wise Collection</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    .Append(T_TotalAmt).Append("</td></tr>");

                    //items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='10' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Day Wise Collection</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    //.Append(T_Count.ToString()).Append("</td></tr>");

                }


                T_TotalAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_Count += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDepartmentWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = odal.FetchDataTableBySP("rptDrwiseopdcollectiondetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;
                previousLabel = dr["DepartmentName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Day Wise Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount).Append("</td></tr>");


                }

                T_Count += dr["TotalAmt"].ConvertToDouble();
            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDepartmentServiceGroupWiseCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
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
            StringBuilder items = new("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount++;
                T_Count++;

                previousLabel = dr["DepartmentName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");

                
              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> TOTAL</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(T_Count).Append("</td></tr>");


                }
                T_Count += dr["NetPayableAmt"].ConvertToDouble();
            }



            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

       

        public string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };

            var Bills = odal.FetchDataTableBySP("rptOPDailyCollectionReport", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new("");
            int i = 0, j = 0;
            string previousLabel = "";
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NETPayAmount = 0, G_PayTMAmount = 0, G_DiscAmount = 0, G_NETAmount = 0, G_PaidAmount = 0, G_BalAmount = 0, G_TotAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0;

          

            double T_BillCash = 0, T_BillCard = 0, T_BillCheque = 0, T_BillNEFT = 0, T_BillPayTm = 0;
            double T_TotAmount = 0, T_DiscAmount = 0, T_NetAmount = 0, T_BalAmount = 0, T_PaidAmount = 0;
            double T_BillReturnCash = 0, T_BillReturnCard = 0, T_BillReturnCheque = 0, T_BillReturnNEFT = 0, T_BillReturnPAYTM = 0;
            double T_BillReturnTot = 0, T_BillReturnDisc = 0, T_BillReturnNet = 0, T_BillReturnPaid = 0, T_BillReturnBal = 0;
            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        // .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        //.Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_TotAmount = 0; G_DiscAmount = 0; G_NETAmount = 0; G_PaidAmount = 0; G_BalAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; double G_NEFTPayAmount = 0;
                    G_PayTMAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");

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
                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();



                previousLabel = dr["Type"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Number"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ReceiptNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_NETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                           .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td></tr>");


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
                    T_BillPayTm += dr["PayTMAmount"].ConvertToDouble();


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
                    T_BillReturnPAYTM += dr["PayTMAmount"].ConvertToDouble();


                }


            }


            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalDisc= T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();
            //T_FinalTotal = T_TotAmount.ConvertToDouble() - T_BillReturnTot.ConvertToDouble();

            double T_TotalCash = T_BillCash.ConvertToDouble() - T_BillReturnCash.ConvertToDouble();
            double T_TotalCard = T_BillCard.ConvertToDouble() - T_BillReturnCard.ConvertToDouble();
            double T_TotalCheque = T_BillCheque.ConvertToDouble() - T_BillReturnCheque.ConvertToDouble();
            double T_TotalNEFT = T_BillNEFT.ConvertToDouble() - T_BillReturnNEFT.ConvertToDouble();
            double T_TotalPAYTM = T_BillPayTm.ConvertToDouble() - T_BillReturnPAYTM.ConvertToDouble();


            double TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble() + T_TotalNEFT.ConvertToDouble() + T_TotalPAYTM.ConvertToDouble();

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


            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.To2DecimalPlace());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName").ToString());

            return html;
        }



        public string ViewOPrefundbilllistReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }

        //public string ViewOPDoctorWiseNewOldPatientReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        //{
        //    throw new NotImplementedException();
        //}
        public string ViewOpPatientCreditList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }
    }
}



