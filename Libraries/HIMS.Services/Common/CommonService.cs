﻿using HIMS.Core.Domain.Grid;
using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Dynamic;

namespace HIMS.Services.Common
{
    public class CommonService : ICommonService
    {
        public CommonService()
        {
        }


        public dynamic GetDDLByIdWithProc(DDLRequestModel model)
        {
            DatabaseHelper odal = new();
            string sp_Name = string.Empty;
            string para_Name = string.Empty;
            SqlParameter[] para = new SqlParameter[1];
            int sp_Para = 0;
            switch (model.Mode)
            {
                case "DeptDoctorList": sp_Name = "ps_getDepartmentWiseDoctorList"; para_Name = "DepartmentId"; break;
                default: break;
            }
            var param = new SqlParameter
            {
                ParameterName = "@" + para_Name,
                Value = model.Id.ToString()
            };
            para[sp_Para] = param;
            DataTable dt = odal.FetchDataTableBySP(sp_Name, para);
            dynamic result = new ExpandoObject();
            if (dt.Rows.Count > 0)
                result = dt.ToDynamic();
            return result;
        }
        public dynamic GetDataSetByProc(ListRequestModel model)
        {
            DatabaseHelper odal = new();
            Dictionary<string, string> fields = SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
            //string SortingField = String.Empty;
            //if (!string.IsNullOrEmpty(model.SortingField?.FieldName)) { SortingField = model.SortingField?.FieldName;}
            //KeyValuePair<string, string> sortingFielsAndDirection = new KeyValuePair<string, string>(SortingField, model.SortingField?.Direction);
            //KeyValuePair<int, int> pageNumberAndCount = new KeyValuePair<int, int>(model.PageNumber, model.PageSize);

            string sp_Name = string.Empty;
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[fields.Count];
            switch (model.Mode)
            {
                //Ashu//
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

                // Check for Dashboard API
                case "DailyDashboardSummary": sp_Name = "rptOP_DepartmentChart_Range"; break;
                case "MISDashboards": sp_Name = "sp_MIS_Dashboards"; break;

                // Pathology Result Entry
                case "PathologyResultEntryOP": sp_Name = "ps_Rtrv_PathologyResultList_ForOPAge"; break;
                case "PathologyResultEntryIP": sp_Name = "ps_Rtrv_PathologyResultList_ForIPAge"; break;

                case "PathologyResultEntryOPCompleted": sp_Name = "ps_Rtrv_PathologyResultList_ForOPAge_Test"; break;
                case "PathologyResultEntryIPCompleted": sp_Name = "ps_Rtrv_PathologyResultList_ForIPAge_Test"; break;

                //System Configuration
                case "SysConfig": sp_Name = "ps_SS_ConfigSettingParam"; break;

                //Pharmacy Sales return
                case "IPSalesReturnCash": sp_Name = "m_Rtrv_IPSalesBillForReturn_Cash"; break;
                case "IPSalesReturnCredit": sp_Name = "m_Rtrv_IPSalesBillForReturn_Credit"; break;
                case "SalesReturnCash": sp_Name = "Retrieve_SalesBill_Return_Cash"; break;
                case "SalesReturnCredit": sp_Name = "Retrieve_SalesBill_Return_Credit"; break;

                // Report - Get Procedure column     
                case "GetProcedureReportcol": sp_Name = "ps_get_ProcedureCol"; break;
                case "GetReportDetailList": sp_Name = "ps_getReportDetaillist"; break;

                // CompanyWiseTraiffList
                case "CompanyWiseTraiffList": sp_Name = "ps_Rtrv_ServiceList_TariffWise"; break;
                case "CompanyWiseSubTPAList": sp_Name = "ps_SubTPACompanyList_CompanyWise"; break;
                case "CompanyWiseServiceList": sp_Name = "ps_Rtrv_ServiceList_CompanyTariffWise"; break;
                case "Departmentdropdown": sp_Name = "ps_Departmentdropdown"; break;
                case "DoctorSurgion": sp_Name = "ps_DoctorSurgionMasterDropdown"; break;
                case "SurgeryMaster": sp_Name = "ps_SurgeryMasterDropdown"; break;
                case "SurgeryType": sp_Name = "ps_SurgeryTypeDropdown"; break;
                case "SiteDescription": sp_Name = "ps_SiteDescriptionDropDown"; break;
                case "SurgeryCategory": sp_Name = "ps_SurgeryCategoryDropDown"; break;









                default: break;
            }
            foreach (var property in fields)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = property.Value.ToString()
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            DataSet ds = odal.FetchDataSetBySP(sp_Name, para);
            dynamic result = new ExpandoObject();
            foreach (DataTable dt in ds.Tables)
            {
                //var dict = (IDictionary<string, object>)result;
                if (dt.Rows.Count > 0)
                    result = dt.ToDynamic();
            }
            return result;
        }

        public List<T> GetSingleListByProc<T>(ListRequestModel model)
        {
            DatabaseHelper odal = new();
            Dictionary<string, string> fields = SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
           
            string sp_Name = string.Empty;
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[fields.Count];
            switch (model.Mode)
            {
                //case "PurchaseOrder": sp_Name = "m_Rtrv_PurchaseOrderList_by_Name_Pagn"; break;
                //case "GRN": sp_Name = "m_Rtrv_GRNList_by_Name"; break;
                //case "OPVisit": sp_Name = "m_Rtrv_VisitDetailsList_1_Pagi"; break;
                //// Check for Dashboard API
                //case "DailyDashboardSummary": sp_Name = "rptOP_DepartmentChart_Range"; break;
                case "MISDashboards": sp_Name = "sp_MIS_Dashboards"; break;
                default: break;
            }
            foreach (var property in fields)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = property.Value.ToString()
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            return odal.FetchListBySP<T>(sp_Name, para);
        }
    }
}
