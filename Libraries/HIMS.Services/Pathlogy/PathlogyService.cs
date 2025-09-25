using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Pathlogy
{
    public class PathlogyService : IPathlogyService
    {
       
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
        public virtual async Task<IPagedList<PathPatientTestListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathPatientTestListDto>(model, "ps_Rtrv_PathPatientList_Ptnt_Dtls");

        }
       
        public virtual async Task InsertAsyncResultEntry(List<TPathologyReportDetail> ObjPathologyReportDetail, TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            foreach (var item in ObjPathologyReportDetail)
            {
                var tokensObj = new
                {
                    PathReportID = Convert.ToInt32(item.PathReportId)
                };

                odal.ExecuteNonQuery("m_Delete_T_PathologyReportDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }

            foreach (var item in ObjPathologyReportDetail)
            {
               
                string[] rEntity = { "PathReportDetId", "Opdipdid", "Opdipdtype" };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_insert_PathRrptDet_1", CommandType.StoredProcedure, entity);
            }
            
                string[] Entity = { "OpdIpdType", "OpdIpdId", "PathTestId", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "SampleNo", "SampleCollectionTime",
                "IsSampleCollection","TestType","IsVerifySign","IsVerifyid","IsVerifyedDate","TPathologyReportDetails","TPathologyReportTemplateDetails","PathDate","PathTime","OutSourceId","OutSourceLabName","OutSourceSampleSentDateTime","OutSourceStatus","OutSourceReportCollectedDateTime","OutSourceCreatedBy","OutSourceCreatedDateTime","OutSourceModifiedby","OutSourceModifiedDateTime","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
                var Hentity = ObjTPathologyReportHeader.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, Hentity);
        }
        public virtual async Task InsertPathPrintResultentry(List<TempPathReportId> ObjTempPathReportId, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            foreach (var item in ObjTempPathReportId)
            {

                var tokensObj = new
                {
                    PathReportId = Convert.ToInt32(item.PathReportId)
                };

                odal.ExecuteNonQuery("m_truncate_Temp_PathReportId", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }
            foreach (var item in ObjTempPathReportId)
            {

                string[] rEntity = Array.Empty<string>();
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_Insert_Temp_PathReportId", CommandType.StoredProcedure, entity);

            }

        }



        public virtual async Task InsertAsyncResultEntry1(TPathologyReportTemplateDetail ObjTPathologyReportTemplateDetail, TPathologyReportHeader ObjTPathologyReportHeader,int UserId, string UserName)
        {
            DatabaseHelper odal = new();
           
            var tokensObj = new
            {
                PathReportID = Convert.ToInt32(ObjTPathologyReportTemplateDetail.PathReportId)
            };
            odal.ExecuteNonQuery("m_Delete_T_PathologyReportTemplateDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "PathReportTemplateDetId", "PathReport" };
            var entity = ObjTPathologyReportTemplateDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_PathologyReportTemplateDetails_1", CommandType.StoredProcedure, entity);

            string[] Entity = { "OpdIpdType", "OpdIpdId", "PathTestId", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "SampleNo", "SampleCollectionTime",
                "IsSampleCollection","TestType","IsVerifySign","IsVerifyid","IsVerifyedDate","TPathologyReportDetails","TPathologyReportTemplateDetails","PathDate","PathTime"};
            var Hentity = ObjTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in Entity)
            {
                Hentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, Hentity);

        }
        public virtual async Task DeleteAsync(TPathologyReportDetail ObjTPathologyReportDetail, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "PathReportDetId","CategoryId","TestId","SubTestId","ParameterId","ResultValue","UnitId","NormalRange","PrintOrder","PisNumeric","CategoryName","TestName","SubTestName","ParameterName","UnitName","PatientName","RegNo",
            "SampleId","MinValue","MaxValue","ParaBoldFlag"};
            var entity = ObjTPathologyReportDetail.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("m_RollBack_TestForResult", CommandType.StoredProcedure, entity);

        }
    }
}
