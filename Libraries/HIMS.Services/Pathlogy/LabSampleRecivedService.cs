using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
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
    public class LabSampleRecivedService : ILabSampleRecivedService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabSampleRecivedService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LabSampleRecivedListDto>> LabGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabSampleRecivedListDto>(model, "ps_Rtrv_LabSampleReceviedList");
        }


        //public virtual async Task Update(List<TPathologyReportHeader> ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        //{

        //    DatabaseHelper odal = new();
        //    foreach (var items in ObjTPathologyReportHeader)
        //    {

        //        string[] AEntity = { "PathReportID", "SampleReceviedDateTime", "SampleReceviedUserId", "IsSampleReceivedStatus" };
        //        var pentity = items.ToDictionary();
        //        foreach (var rProperty in pentity.Keys.ToList())
        //        {
        //            if (!AEntity.Contains(rProperty))
        //                pentity.Remove(rProperty);
        //        }


        //        odal.ExecuteNonQuery("ps_UpdateLabSampleRecived", CommandType.StoredProcedure, pentity);
        //        await _context.LogProcedureExecution(pentity, nameof(TPathologyReportHeader), Convert.ToInt32(items.PathReportId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
        //    }
        //}
        public virtual async Task Update(List<TPathologyReportHeader> ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)

        {
            DatabaseHelper odal = new();

            foreach (var item in ObjTPathologyReportHeader)
            {
                Dictionary<string, object> parameters = new()
                {
                    ["PathReportID"] = item.PathReportId,
                    ["SampleReceviedDateTime"] = item.SampleReceviedDateTime,
                    ["SampleReceviedUserId"] = item.SampleReceviedUserId,
                    ["IsSampleReceivedStatus"] = item.IsSampleReceivedStatus
                };

                odal.ExecuteNonQuery(  "ps_UpdateLabSampleRecived", CommandType.StoredProcedure, parameters);
            }
        }
        public virtual async Task DeleteAsync(TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "PathReportId", "SampleReceviedCancelReason" };
            var entity = ObjTPathologyReportHeader.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_PathologyReportHeaderCancel", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TPathologyReportHeader), Convert.ToInt32(ObjTPathologyReportHeader.PathReportId), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

        }

    }
}
