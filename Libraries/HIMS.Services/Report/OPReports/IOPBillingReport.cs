using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Report.OPReports
{
    public interface IOPBillingReport
    {
        string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeaderFilePath);
        string ViewRegistrationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseVisitReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewRefDoctorWiseReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCrossConsultationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPAppointmentListReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWisecountSummury(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPAppoinmentListWithServiseAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPDoctorWiseNewOldPatientReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDayWiseOpdCountDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDayWiseOpdCountSummry(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOpdCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseOpdCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewBillReportSummary(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeader);
        string ViewBillReportSummarySummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPDBillBalanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPDRefundOfBill(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentServiceGroupWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);




        string ViewDepartmentWiseOPDCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDrWiseOPDCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDrWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentServiceGroupWiseCollectionDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);


        string ViewOpPatientCreditList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);

        string ViewOPrefundbilllistReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);

    }
}
