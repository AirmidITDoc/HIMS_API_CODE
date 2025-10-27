using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
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
            return await DatabaseHelper.GetGridDataBySp<PathRadServiceListDto>(model, "Rtrv_PathRadServiceList");

        }
        public virtual async Task UpdateAsyncSP(List<TPathologyReportHeader> objTPathologyReportHeader, int UserId, string Username)
        {
            DatabaseHelper odal = new();

            foreach (var item in objTPathologyReportHeader)
            {
                //string[] rEntity = { "PathDate","PathTime","OpdIpdType", "OpdIpdId", "PathTestId", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId",  "IsCompleted", "IsPrinted", "ReportDate", "ReportTime", "IsTemplateTest", "TestType",
                //                     "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "IsVerifySign", "IsVerifyid", "IsVerifyedDate", "TPathologyReportDetails",  "TPathologyReportTemplateDetails", "OutSourceId","OutSourceLabName","OutSourceSampleSentDateTime","OutSourceStatus","OutSourceReportCollectedDateTime","OutSourceCreatedBy","OutSourceCreatedDateTime","OutSourceModifiedby","OutSourceModifiedDateTime","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
                //var entity = item.ToDictionary();
                //foreach (var rProperty in rEntity)
                //{
                //    entity.Remove(rProperty);
                //}

                string[] AEntity = { "PathReportId", "SampleCollectionTime", "IsSampleCollection", "SampleNo", "SampleCollectedBy" };
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

    }
}
