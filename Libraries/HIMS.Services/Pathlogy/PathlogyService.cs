using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class PathlogyService : IPathlogyService
    {
        public virtual async Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateForUpdateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTemplateForUpdateListDto>(model, "Rtrv_PathTemplateForUpdate");
        }
      
        public virtual async Task<IPagedList<PathParaFillListDto>> PathParaFillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathParaFillListDto>(model, "rtrv_PathParaFill");
        }
        public virtual async Task<IPagedList<PathSubtestFillListDto>> PathSubtestFillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathSubtestFillListDto>(model, "rtrv_PathSubtestFill");
        }
       
        public virtual async Task<IPagedList<PathResultEntryListDto>> PathResultEntry(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathResultEntryListDto>(model, "ps_Rtrv_PathResultEntryList_Test_Dtls");
        }
        public virtual async Task<IPagedList<PatientTestListDto>> PGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientTestListDto>(model, "ps_Rtrv_PathPatientList_Ptnt_Dtls");

        }
        public virtual async Task<IPagedList<LabOrRadRequestListDto>> LGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabOrRadRequestListDto>(model, "Rtrv_LabOrRadRequestList");

        }

        public virtual async Task<IPagedList<TestListDTo>> GetPathTestListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TestListDTo>(model, "m_Rtrv_PathologyTestList");
        }
        public virtual async Task InsertAsyncResultEntry(TPathologyReportDetail ObjPathologyReportDetail, List<TPathologyReportHeader> ObjTPathologyReportHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            var tokensObj = new
            {
                PathReportID = Convert.ToInt32(ObjPathologyReportDetail.PathReportId)
            };
            odal.ExecuteNonQuery("m_Delete_T_PathologyReportDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "PathReportDetId", "PathReport" };
            var entity = ObjPathologyReportDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_PathRrptDet_1", CommandType.StoredProcedure, entity);
            foreach (var item in ObjTPathologyReportHeader)
            {
            string[] Entity = { "OpdIpdType", "OpdIpdId", "PathTestId", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "SampleNo", "SampleCollectionTime",
                "IsSampleCollection","TestType","IsVerifySign","IsVerifyid","IsVerifyedDate","TPathologyReportDetails","TPathologyReportTemplateDetails","PathDate","PathTime"};
                var Hentity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, Hentity);
            }
        }
    }
}
