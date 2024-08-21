using HIMS.Core.Domain.Grid;
using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
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
                case "PurchaseOrder": sp_Name = "m_Rtrv_PurchaseOrderList_by_Name_Pagn"; break;
                case "GRN": sp_Name = "m_Rtrv_GRNList_by_Name"; break;
                case "OPVisit": sp_Name = "m_Rtrv_VisitDetailsList_1_Pagi"; break;
                // Check for Dashboard API
                case "DailyDashboardSummary": sp_Name = "rptOP_DepartmentChart_Range"; break;
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
            DataSet ds = odal.FetchDataSetBySP(sp_Name, para);
            dynamic result = new ExpandoObject();
            foreach (DataTable dt in ds.Tables)
            {
                var dict = (IDictionary<string, object>)result;
                if (dt.Rows.Count > 0)
                    dict[dt.TableName] = dt.ToDynamic();
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
