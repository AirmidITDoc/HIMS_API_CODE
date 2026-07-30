using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CommonController : BaseController
    {
        private readonly ICommonService _ICommonService;
        public CommonController(ICommonService commonRepository)
        {
            _ICommonService = commonRepository;
        }

        [HttpPost]
        public ApiResponse GetByProc(ListRequestModel model)
        {
            string sp_Name = string.Empty;
            switch (model.Mode)
            {

                case "OpeningItemDet": sp_Name = "m_Rtrv_OpeningItemDet"; break;
                case "OpeningItemList": sp_Name = "m_Rtrv_OpeningItemList"; break;
                case "GrnItemList": sp_Name = "Retrieve_GrnItemList"; break;
                case "GRNList": sp_Name = "m_Rtrv_GRNList_by_Name"; break;
                case "PurchaseItem": sp_Name = "m_Rtrv_PurchaseItemList"; break;
                case "PurchasesOrder": sp_Name = "Rtrv_LastThreeItemInfo"; break;
                case "PurchaseOrder": sp_Name = "m_Rtrv_PurchaseOrderList_by_Name_Pagn"; break;
                case "GRN": sp_Name = "m_Rtrv_GRNList_by_Name"; break;
                case "OPVisit": sp_Name = "m_Rtrv_VisitDetailsList_1_Pagi"; break;

                case "OPDEMR": sp_Name = "m_rtrv_CertificateMasterCombo"; break;

                // Check IP admission
                case "CheckPatientAdmitted": sp_Name = "ps_CheckPatientAdmitted"; break;

                // Check for Dashboard API
                case "DailyDashboardSummary": sp_Name = "rptOP_DepartmentChart_Range"; break;
                case "MISDashboards": sp_Name = "sp_MIS_Dashboards"; break;

                // Pathology Result Entry
                case "PathologyResultEntryOP": sp_Name = "ps_Rtrv_PathologyResultList_ForOPAge"; break;
                case "PathologyResultEntryIP": sp_Name = "ps_Rtrv_PathologyResultList_ForIPAge"; break;
                case "PathologyResultEntryLAB": sp_Name = "ps_Rtrv_PathologyResultList_ForLABAge"; break;

                case "PathologyResultEntryOPCompleted": sp_Name = "ps_Rtrv_PathologyResultList_ForOPAge_Test"; break;
                case "PathologyResultEntryIPCompleted": sp_Name = "ps_Rtrv_PathologyResultList_ForIPAge_Test"; break;
                case "PathologyResultEntryLabCompleted": sp_Name = "ps_Rtrv_PathologyResultList_ForLABAge_Test"; break;

                case "PathologyResultEntryOPMachine": sp_Name = "ps_Rtrv_PathologyResultList_ForOPAgeMachine"; break;
                case "PathologyResultEntryIPMachine": sp_Name = "ps_Rtrv_PathologyResultList_ForIPAgeMachine"; break;
                case "PathologyResultEntryLabMachine": sp_Name = "ps_Rtrv_PathologyResultList_ForLABAgeMachine"; break;

                case "LabCreditBillList": sp_Name = "ps_Lab_CreditBillList"; break;
                case "LabBillHistoryList": sp_Name = "ps_Lab_BillDetailsList"; break;
                case "LabSampletracker": sp_Name = "ps_SampleCollectiontracker"; break;

                //
                case "OPBillPrint": sp_Name = "ps_rptBillPrint"; break;
                case "BillList": sp_Name = "ps_rtrv_BillList"; break;

                // Check IP admission
                case "GetBillDetails": sp_Name = "ps_getBillDetails"; break;

                //////System Configuration

                case "NewSysConfig": sp_Name = "m_SS_ConfigSettingParam"; break;

                //Pharmacy Sales return
                case "IPSalesReturnCash": sp_Name = "m_Rtrv_IPSalesBillForReturn_Cash"; break;
                case "IPSalesReturnCredit": sp_Name = "m_Rtrv_IPSalesBillForReturn_Credit"; break;
                case "IPSalesInPatientReturnCredit": sp_Name = "ps_Rtrv_IPSalesInPatientBillForReturn_Credit"; break;
                case "SalesReturnCash": sp_Name = "Retrieve_SalesBill_Return_Cash"; break;
                case "SalesReturnCredit": sp_Name = "Retrieve_SalesBill_Return_Credit"; break;

                // Report - Get Procedure column     
                case "GetProcedureReportcol": sp_Name = "ps_get_ProcedureCol"; break;
                case "GetReportDetailList": sp_Name = "ps_getReportDetaillist"; break;

                // CompanyWiseTraiffList
                case "CompanyWiseTraiffList": sp_Name = "ps_Rtrv_ServiceList_TariffWise"; break;
                case "CompanyWiseSubTPAList": sp_Name = "ps_SubTPACompanyList_CompanyWise"; break;
                case "CompanyWiseServiceList": sp_Name = "ps_Rtrv_ServiceList_CompanyTariffWise"; break;

                // Sysytem Config and Login Access
                case "LoginAccessConfigList": sp_Name = "ps_M_LoginAccessConfigList"; break;
                case "SystemConfigList": sp_Name = "ps_M_SystemConfigList"; break;
                case "UnitWiseSystemConfige": sp_Name = "ps_UnitWiseSystemConfige"; break;
                case "LoginWiseAccessConfigList": sp_Name = "ps_LoginWise_LoginAccessConfigList"; break;


                //GSTType  
                case "grnInvoicenocheck": sp_Name = "ps_m_grnInvoiceno_check"; break;
                case "CheckExistingBatchAvailable": sp_Name = "ps_CheckExistingBatchAvailable"; break;
                case "ExpHeadMaster": sp_Name = "Retrieve_M_ExpHeadMasterForCombo"; break;
                case "TemplateDescCategory": sp_Name = "ps_TemplateDescCategoryList"; break;

                // Applicaation Dashboard App API
                case "HomeDashboardAPI": sp_Name = "ps_DASH_APPOINTMENT_COUNT"; break;
                case "DashOPDepatmentWiseCount": sp_Name = "ps_DASH_OP_DEPARTMENTCOUNT"; break;
                case "DashOPConsultantWiseCount": sp_Name = "ps_DASH_OP_ConsultantDoctorWise_COUNT"; break;
                case "DashOPUserWiseRevenue": sp_Name = "ps_DASH_OP_BILL_PAYMENT_SUMMARY"; break;
                case "DashRegistrationAgeWiseCount": sp_Name = "ps_DASH_RegistrationAgeWise_COUNT"; break;
                case "DashOPAppointmentNewOrOld": sp_Name = "ps_Dash_OPAppointmentNewOrOld_1"; break;

                case "DashWardWiseBed": sp_Name = "ps_Dash_WardWiseBedOccupancy_1"; break;
                case "DashBedWiseList": sp_Name = "ps_Dash_BedWiseList_1"; break;
                case "DashBedStatistics": sp_Name = "ps_Dash_Bed_statistics_1"; break;
                case "DashAdmissionDateWiseCount": sp_Name = "ps_Dash_AdmissionCountLessthan15Day_1"; break;
                case "DashDischargeDateWiseCount": sp_Name = "ps_Dash_DischargeCountLessthan15Day_1"; break;


                // Pathology Dashboard  
                case "PathologyDashboard": sp_Name = "ps_rpt_PathologyDashboard"; break;
                // Radiology Dashboard  
                case "RadiologyDashboard": sp_Name = "ps_rpt_RadiologyDashboard"; break;


                // Admin Task for Update dates and times
                case "Admin_Visitlist": sp_Name = "ps_Admin_VisitList"; break;
                case "Admin_VisitWiseBilllist": sp_Name = "ps_Admin_VisitWiseBillList"; break;
                case "Admin_VisitBillWisePaymentlist": sp_Name = "ps_Admin_VisitWiseBillPaymentList"; break;
                case "Admin_VisitRefundBillWiselist": sp_Name = "ps_Admin_VisitWiseRefundBillList"; break;
                case "Admin_VisitAdvanceWiselist": sp_Name = "ps_Admin_VisitWiseAdvanceList"; break;


                // AirmidMobile App API
                case "Mobile_PatientRegistration": sp_Name = "ps_MobileApp_HomePage_PatientRegistration"; break;
                case "Mobile_AppointmentAdmissionSummary": sp_Name = "ps_MobileApp_HomePage_AppointmentAdmissionSummary"; break;
                case "Mobile_WardWiseBedOccupancy": sp_Name = "ps_MobileApp_WardWiseBedOccupancy"; break;
                case "Mobile_DoctorWisePerformance": sp_Name = "ps_rpt_DoctorWisePatientCount"; break;
                case "Mobile_DepartmentWisePerformance": sp_Name = "ps_rpt_DepartmentWisePatientCount"; break;
                case "Mobile_OPIPBillingList": sp_Name = "ps_APP_BILL_OP_IP_LIST"; break;
                case "Mobile_OPIPBillDetails": sp_Name = "ps_APP_VIEW_BILL_DET"; break;
                case "Mobile_ViewPathologytestDet": sp_Name = "ps_APP_VIEW_PathologyTest_DET"; break;
                case "Mobile_ViewIPInvestigationlist": sp_Name = "ps_Rtrv_IPInvestigation_List"; break;



                // Marketing Mobile App API
                case "MarketingTodayVisitCount": sp_Name = "ps_Marketing_App_TodayVisitCount"; break;
                case "MarketingTodayVisitCityWiseCount": sp_Name = "ps_Marketing_App_TodayVisitCityWiseCount"; break;
                case "MarketingTodayVisitCategoryWiseCount": sp_Name = "ps_Marketing_App_TodayVisitCategoryWiseCount"; break;
                case "MarketingTodayVisitPersonWiseCount": sp_Name = "ps_Marketing_App_TodayVisitPersonWiseCount"; break;
                case "ItemSupplierDetails": sp_Name = "ps_Rtrv_LastThreeSupplierInfo"; break;
                case "ConstantType": sp_Name = "m_rtrv_ConstantType_Wise_List"; break;
                case "paymentMode": sp_Name = "ps_rtrv_paymentModelist"; break;
                case "ParameterDescriptiveMaster": sp_Name = "ps_Get_ParameterDescriptiveMaster_ById"; break;
                case "OPBillPaymentListForPayModeChange": sp_Name = "ps_rtrv_OPBillPaymentListForPayModeChange"; break;
                case "subQuestionList": sp_Name = "ps_Rtrv_subQuestionList"; break;
                case "subQuestionValueList": sp_Name = "ps_Rtrv_subQuestionValueList"; break;
                case "ClinicalQuesDetail": sp_Name = "ps_Rtrv_ClinicalQuesDetail_Test"; break;
                case "PaymentMode": sp_Name = "Rtrv_ConstantPayMode"; break;

                case "BankNameList": sp_Name = "ps_Rtrv_BankMaster"; break;
                case "AdmissionCancleStaus": sp_Name = "Check_AdmissionCancleStaus"; break;
                case "PharmacyAmtByAdminId": sp_Name = "Rtrv_PharmacyAmtByAdminId"; break;
                case "PCPNDTIndicationList": sp_Name = "ps_RtrvPCPNDT_IndicationList"; break;




                default: break;
            }
            dynamic resultList = _ICommonService.GetDataSetByProc(sp_Name,  model.SearchFields);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", (dynamic)resultList);
        }
        [Route("get-daily-dashboard-data")]
        [HttpPost]
        public ApiResponse GetListByProc(ListRequestModel model)
        {
            string pDashboardId = model?.SearchFields?.FirstOrDefault(x => x.FieldName == "pDashboardId")?.FieldValue ?? "";
            if (pDashboardId == "1")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Mode + " List.", _ICommonService.GetSingleListByProc<DashboardDto1>(model));
            else if (pDashboardId == "2")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Mode + " List.", _ICommonService.GetSingleListByProc<DashboardHospitalDto>(model));
            else if (pDashboardId == "3")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Mode + " List.", _ICommonService.GetSingleListByProc<DashboardDoctorDto>(model));
            else if (pDashboardId == "3")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Mode + " List.", _ICommonService.GetSingleListByProc<DashboardOperativeDto>(model));
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "invalid data");
        }
    }
    public class DashboardDto1
    {
        public DateTime? BillDate { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public string Ipd { get; set; }
        public string Opd { get; set; }
        public string Pharma { get; set; }
        public string Total { get { return Convert.ToString(Convert.ToDecimal(Ipd) + Convert.ToDecimal(Opd) + Convert.ToDecimal(Pharma)); } }
    }
    public class DashboardHospitalDto
    {
        public DateTime? BillDate { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public int HospitalPatient { get; set; }
        public int PrivatePatient { get; set; }
        public int ReferalPatient { get; set; }
        public int Total { get { return HospitalPatient + PrivatePatient + ReferalPatient; } }
        public decimal HospitalPatientPer { get { return Convert.ToDecimal(((decimal)HospitalPatient * 100 / Total).ToString("F")); } }
        public decimal PrivatePatientPer { get { return Convert.ToDecimal(((decimal)PrivatePatient * 100 / Total).ToString("F")); } }
        public decimal ReferalPatientPer { get { return Convert.ToDecimal(((decimal)ReferalPatient * 100 / Total).ToString("F")); } }
    }
    public class DashboardDoctorDto
    {
        public string DoctorName { get; set; }
        public int TotalPatients { get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
        public decimal PerJan { get; set; }
        public decimal PerFeb { get; set; }
        public decimal PerMar { get; set; }
        public decimal PerApr { get; set; }
        public decimal PerMay { get; set; }
        public decimal PerJun { get; set; }
        public decimal PerJul { get; set; }
        public decimal PerAug { get; set; }
        public decimal PerSep { get; set; }
        public decimal PerOct { get; set; }
        public decimal PerNov { get; set; }
        public decimal PerDec { get; set; }
    }
    public class DashboardOperativeDto
    {
        public string BillMonthName { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public int Medicine { get; set; }
        public int Operative { get; set; }
        public int Total { get { return Medicine + Operative; } }
        public decimal PerOperative { get { return Convert.ToDecimal(((decimal)Operative * 100 / Total).ToString("F")); } }
        public decimal PerMedicine { get { return Convert.ToDecimal(((decimal)Medicine * 100 / Total).ToString("F")); } }
    }
}
