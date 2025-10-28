namespace HIMS.Services.Report.IPReports
{
    public interface IIPReport
    {
        string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string HeaderName);


        string ViewIPDAdvanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewBillReport(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string HeaderName);
        string ViewBillSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewRefundofAdvanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewRefundofBillReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDischargeAndBillGenerationPendingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewOPIPBillSummaryReceipt(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewIPCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        //string ViewIPDAdmissionWardWiseCharges(int DoctorId, int WardId,int CompanyId, string htmlFilePath, string HeaderName);

        //string ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        //string ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeTypeCompanyWise(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeTypeCompanyWiseCount(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId, string htmlFilePath, string HeaderName);
        string ViewIPDRefDoctorWise(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeDetails(DateTime FromDate, DateTime ToDate, int DischargeTypeId, string htmlFilePath, string HeaderName);




        string ViewIPDAdmissionList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string ViewIPDAdmissionListCompanyWiseDetails(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string HeaderName);
        string ViewIPDCurrentAdmittedList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId, string htmlFilePath, string HeaderName);
        string ViewIPDCurrentAdmittedWardWiseCharges(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeReportWithMarkStatus(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeReportWithBillSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewCurrentrefadmittedlist(int DoctorId, string htmlFilePath, string HeaderName);

        string ViewIPDDischargetypewise(int DoctorId, DateTime FromDate, DateTime ToDate, int DischargeTypeId, string htmlFilePath, string HeaderName);


        string ViewOPToIPConvertedListWithServiceAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        //IPD MIS REPORTS

        string ViewDateWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewMonthWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDoctorWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDoctorWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDepartmentWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDepartmentWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDrWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDrWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseAdmissionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseBillDetailReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseBillSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseCreditReportDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseCreditReportSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPAdmitPatientwardwisechargesReport(int DoctorId, int WardId, int CompanyId, string htmlFilePath, string HeaderName);
    }
}