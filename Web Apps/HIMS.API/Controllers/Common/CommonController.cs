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
        private static readonly IReadOnlyDictionary<string, string> ProcedureMappings =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["OpeningItemDet"] = "m_Rtrv_OpeningItemDet",
        ["OpeningItemList"] = "m_Rtrv_OpeningItemList",
        ["GrnItemList"] = "Retrieve_GrnItemList",
        ["GRNList"] = "m_Rtrv_GRNList_by_Name",
        ["PurchaseItem"] = "m_Rtrv_PurchaseItemList",
        ["PurchasesOrder"] = "Rtrv_LastThreeItemInfo",
        ["PurchaseOrder"] = "m_Rtrv_PurchaseOrderList_by_Name_Pagn",
        ["GRN"] = "m_Rtrv_GRNList_by_Name",
        ["OPVisit"] = "m_Rtrv_VisitDetailsList_1_Pagi",

        ["OPDEMR"] = "m_rtrv_CertificateMasterCombo",

        ["CheckPatientAdmitted"] = "ps_CheckPatientAdmitted",

        ["DailyDashboardSummary"] = "rptOP_DepartmentChart_Range",
        ["MISDashboards"] = "sp_MIS_Dashboards",

        ["PathologyResultEntryOP"] = "ps_Rtrv_PathologyResultList_ForOPAge",
        ["PathologyResultEntryIP"] = "ps_Rtrv_PathologyResultList_ForIPAge",
        ["PathologyResultEntryLAB"] = "ps_Rtrv_PathologyResultList_ForLABAge",

        ["PathologyResultEntryOPCompleted"] = "ps_Rtrv_PathologyResultList_ForOPAge_Test",
        ["PathologyResultEntryIPCompleted"] = "ps_Rtrv_PathologyResultList_ForIPAge_Test",
        ["PathologyResultEntryLabCompleted"] = "ps_Rtrv_PathologyResultList_ForLABAge_Test",

        ["PathologyResultEntryOPMachine"] = "ps_Rtrv_PathologyResultList_ForOPAgeMachine",
        ["PathologyResultEntryIPMachine"] = "ps_Rtrv_PathologyResultList_ForIPAgeMachine",
        ["PathologyResultEntryLabMachine"] = "ps_Rtrv_PathologyResultList_ForLABAgeMachine",

        ["LabCreditBillList"] = "ps_Lab_CreditBillList",
        ["LabBillHistoryList"] = "ps_Lab_BillDetailsList",
        ["LabSampletracker"] = "ps_SampleCollectiontracker",

        ["OPBillPrint"] = "ps_rptBillPrint",
        ["BillList"] = "ps_rtrv_BillList",
        ["GetBillDetails"] = "ps_getBillDetails",

        ["NewSysConfig"] = "m_SS_ConfigSettingParam",

        ["IPSalesReturnCash"] = "m_Rtrv_IPSalesBillForReturn_Cash",
        ["IPSalesReturnCredit"] = "m_Rtrv_IPSalesBillForReturn_Credit",
        ["IPSalesInPatientReturnCredit"] = "ps_Rtrv_IPSalesInPatientBillForReturn_Credit",
        ["SalesReturnCash"] = "Retrieve_SalesBill_Return_Cash",
        ["SalesReturnCredit"] = "Retrieve_SalesBill_Return_Credit",

        ["GetProcedureReportcol"] = "ps_get_ProcedureCol",
        ["GetReportDetailList"] = "ps_getReportDetaillist",

        ["CompanyWiseTraiffList"] = "ps_Rtrv_ServiceList_TariffWise",
        ["CompanyWiseSubTPAList"] = "ps_SubTPACompanyList_CompanyWise",
        ["CompanyWiseServiceList"] = "ps_Rtrv_ServiceList_CompanyTariffWise",

        ["LoginAccessConfigList"] = "ps_M_LoginAccessConfigList",
        ["SystemConfigList"] = "ps_M_SystemConfigList",
        ["UnitWiseSystemConfige"] = "ps_UnitWiseSystemConfige",
        ["LoginWiseAccessConfigList"] = "ps_LoginWise_LoginAccessConfigList",

        ["grnInvoicenocheck"] = "ps_m_grnInvoiceno_check",
        ["CheckExistingBatchAvailable"] = "ps_CheckExistingBatchAvailable",
        ["ExpHeadMaster"] = "Retrieve_M_ExpHeadMasterForCombo",
        ["TemplateDescCategory"] = "ps_TemplateDescCategoryList",

        ["HomeDashboardAPI"] = "ps_DASH_APPOINTMENT_COUNT",
        ["DashOPDepatmentWiseCount"] = "ps_DASH_OP_DEPARTMENTCOUNT",
        ["DashOPConsultantWiseCount"] = "ps_DASH_OP_ConsultantDoctorWise_COUNT",
        ["DashOPUserWiseRevenue"] = "ps_DASH_OP_BILL_PAYMENT_SUMMARY",
        ["DashRegistrationAgeWiseCount"] = "ps_DASH_RegistrationAgeWise_COUNT",
        ["DashOPAppointmentNewOrOld"] = "ps_Dash_OPAppointmentNewOrOld_1",

        ["DashWardWiseBed"] = "ps_Dash_WardWiseBedOccupancy_1",
        ["DashBedWiseList"] = "ps_Dash_BedWiseList_1",
        ["DashBedStatistics"] = "ps_Dash_Bed_statistics_1",
        ["DashAdmissionDateWiseCount"] = "ps_Dash_AdmissionCountLessthan15Day_1",
        ["DashDischargeDateWiseCount"] = "ps_Dash_DischargeCountLessthan15Day_1",

        ["PathologyDashboard"] = "ps_rpt_PathologyDashboard",
        ["RadiologyDashboard"] = "ps_rpt_RadiologyDashboard",

        ["Admin_Visitlist"] = "ps_Admin_VisitList",
        ["Admin_VisitWiseBilllist"] = "ps_Admin_VisitWiseBillList",
        ["Admin_VisitBillWisePaymentlist"] = "ps_Admin_VisitWiseBillPaymentList",
        ["Admin_VisitRefundBillWiselist"] = "ps_Admin_VisitWiseRefundBillList",
        ["Admin_VisitAdvanceWiselist"] = "ps_Admin_VisitWiseAdvanceList",

        ["Mobile_PatientRegistration"] = "ps_MobileApp_HomePage_PatientRegistration",
        ["Mobile_AppointmentAdmissionSummary"] = "ps_MobileApp_HomePage_AppointmentAdmissionSummary",
        ["Mobile_WardWiseBedOccupancy"] = "ps_MobileApp_WardWiseBedOccupancy",
        ["Mobile_DoctorWisePerformance"] = "ps_rpt_DoctorWisePatientCount",
        ["Mobile_DepartmentWisePerformance"] = "ps_rpt_DepartmentWisePatientCount",
        ["Mobile_OPIPBillingList"] = "ps_APP_BILL_OP_IP_LIST",
        ["Mobile_OPIPBillDetails"] = "ps_APP_VIEW_BILL_DET",
        ["Mobile_ViewPathologytestDet"] = "ps_APP_VIEW_PathologyTest_DET",
        ["Mobile_ViewIPInvestigationlist"] = "ps_Rtrv_IPInvestigation_List",

        ["MarketingTodayVisitCount"] = "ps_Marketing_App_TodayVisitCount",
        ["MarketingTodayVisitCityWiseCount"] = "ps_Marketing_App_TodayVisitCityWiseCount",
        ["MarketingTodayVisitCategoryWiseCount"] = "ps_Marketing_App_TodayVisitCategoryWiseCount",
        ["MarketingTodayVisitPersonWiseCount"] = "ps_Marketing_App_TodayVisitPersonWiseCount",

        ["ItemSupplierDetails"] = "ps_Rtrv_LastThreeSupplierInfo",
        ["ConstantType"] = "m_rtrv_ConstantType_Wise_List",
        ["paymentMode"] = "ps_rtrv_paymentModelist",
        ["ParameterDescriptiveMaster"] = "ps_Get_ParameterDescriptiveMaster_ById",
        ["OPBillPaymentListForPayModeChange"] = "ps_rtrv_OPBillPaymentListForPayModeChange",
        ["subQuestionList"] = "ps_Rtrv_subQuestionList",
        ["subQuestionValueList"] = "ps_Rtrv_subQuestionValueList",
        ["ClinicalQuesDetail"] = "ps_Rtrv_ClinicalQuesDetail_Test",
        ["PaymentMode"] = "Rtrv_ConstantPayMode",
        ["BankNameList"] = "ps_Rtrv_BankMaster",
        ["AdmissionCancleStaus"] = "Check_AdmissionCancleStaus",
        ["PharmacyAmtByAdminId"] = "Rtrv_PharmacyAmtByAdminId",
        ["PCPNDTIndicationList"] = "ps_RtrvPCPNDT_IndicationList",
        ["PathologyResultListabnormal"] = "ps_Rtrv_PathologyResultList_abnormal",
        ["DoctorWiseCharges"] = "ps_rtrv_getDoctorWiseCharges",
        ["BrowseIPRefundAdvanceAdmin"] = "ps_Rtrv_BrowseIPRefundAdvance_Admin",



    };
        public CommonController(ICommonService commonRepository)
        {
            _ICommonService = commonRepository;
        }


        [HttpPost]
        public ApiResponse GetByProc(ListRequestModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ProcedureMappings.TryGetValue(model.Mode ?? string.Empty, out var procedureName))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, $"Invalid Mode: {model.Mode}", null);
            }
            var result = _ICommonService.GetDataSetByProc(procedureName, model.SearchFields);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, string.Empty, result);
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
        [HttpPost]
        [Route("get-data-table-by-proc")]
        public ApiResponse GetDataTableByProc(ListRequestModel model)
        {
            string SpName = "";
            switch (model.Mode)
            {
                case "GetList": SpName = "GETLIST_VIMAL"; break;
                case "IPAdvanceRefundPayment": SpName = "ps_NewTally_IPAdvanceRefund_Payment_Mediforte"; break;
                case "OPBillRefundPayment": SpName = "ps_NewTally_OPBillRefund_Payment_Mediforte"; break;
                case "IPBillPayment": SpName = "ps_NewTally_IPBill_Payment_Mediforte"; break;
                case "IPAdvancePayment": SpName = "ps_NewTally_IPAdvance_Payment_Mediforte"; break;

                case "OPPayment": SpName = "ps_NewTally_OP_Payment_Mediforte"; break;
                case "IPBillList": SpName = "ps_NewTally_IPBillList_Mediforte"; break;
                case "IPBillDetailList": SpName = "ps_NewTally_IPBillDetailList_Mediforte"; break;
                case "IPBillRefundPayment": SpName = "ps_NewTally_IPBillRefund_Payment_Mediforte"; break;
                case "OPIPSalsePayment": SpName = "ps_NewTally_OPIPSalsePayment_Mediforte"; break;
                case "OPIPSalesDetailList": SpName = "PS_NewTally_OP_IP_Sales_DetailList_Mediforte"; break;
                case "OPIPSalesReturnBillDetailList": SpName = "ps_NewTally_OPIP_Sales_ReturnBillDetailList_Mediforte"; break;





                default: break;
            }
            dynamic resultList = _ICommonService.GetDataTableByProc(SpName, model.SearchFields);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", resultList);
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
