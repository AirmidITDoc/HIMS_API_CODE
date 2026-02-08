using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HIMS.Services.IPPatient
{
    public class PathlogySampleCollectionService : IPathlogySampleCollectionService
    {
        private readonly HIMSDbContext _context;
        public PathlogySampleCollectionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SampleCollectionPatientListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SampleCollectionPatientListDto>(model, "ps_Rtrv_PathSamPatList");

        }
        public virtual async Task<IPagedList<SampleCollectionTestListDto>> GetListAsyn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SampleCollectionTestListDto>(model, "ps_Rtrv_PathSamColllist_Pat_Dtls");

        }


        public virtual async Task<IPagedList<LabOrRadRequestListDto>> LGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabOrRadRequestListDto>(model, "Rtrv_LabOrRadRequestList");

        }
        public virtual async Task<IPagedList<LabOrRadRequestDetailListDto>> LGetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabOrRadRequestDetailListDto>(model, "ps_Rtrv_NursingPathRadReqDet");

        }
        public virtual async Task<IPagedList<PathRadServiceListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathRadServiceListDto>(model, "ps_Rtrv_PathRadServiceList");

        }
        public virtual async Task UpdateAsyncSP(List<TPathologyReportHeader> objTPathologyReportHeader, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            int nextSampleNo =(await _context.TPathologyReportHeaders.Where(x => x.SampleNo != null && x.SampleNo != "").Select(x => (int?)Convert.ToInt32(x.SampleNo)).MaxAsync() ?? 0) + 1;
            int nextOrderNo =(await _context.TPathologyReportHeaders.Where(x => x.OrderNo != null).MaxAsync(x => (int?)x.OrderNo) ?? 0) + 1;

            foreach (var item in objTPathologyReportHeader)
            {
                item.SampleNo = nextSampleNo.ToString();
                item.OrderNo = nextOrderNo;
                string[] AEntity = { "PathReportId", "SampleCollectionTime", "IsSampleCollection", "SampleNo", "SampleCollectedBy", "OrderNo" };
                var entity = item.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_PathologySampleCollection_1", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, "SampleCollection", item.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, Username);
            }
        }

        public virtual async Task UpdateAsync(TPathologyReportHeader objTPathologyReportHeader, int UserId, string Username)
        {
                DatabaseHelper odal = new();
                string[] AEntity = {  "SampleNo", "SampleCollectionTime" };
                var entity = objTPathologyReportHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_samplecollectionDatetime", CommandType.StoredProcedure, entity);
              //  await _context.LogProcedureExecution(entity, "SampleCollection", objTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, Username);
            
        }

    }
}
