using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;


namespace HIMS.Services.Pathlogy
{
    public class PathlogyService : IPathlogyService
    {
        private readonly HIMSDbContext _context;

        public PathlogyService(HIMSDbContext context)
        {
            _context = context;
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

                odal.ExecuteNonQuery("ps_Delete_T_PathologyReportDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }

            foreach (var item in ObjPathologyReportDetail)
            {
               
                string[] rEntity = { "PathReportId", "CategoryId", "TestId", "SubTestId", "ParameterId", "ResultValue", "UnitId", "NormalRange", "PrintOrder", "PisNumeric", "CategoryName", "TestName", "SubTestName", "ParameterName", "UnitName", "PatientName", "RegNo", "SampleId", "ParaBoldFlag", "MinValue", "MaxValue", "Opipnumber", "AgeY", "AgeM", "AgeD", "GenderId", "SampleNo", "SuggestionNotes"};
                var entity = item.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_PathRrptDet_1", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, "PathologyTemplate", item.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, UserName);

            }

                string[] Entity = { "PathReportId", "ReportDate", "ReportTime", "IsCompleted", "IsPrinted", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsTemplateTest", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId"};
                var Hentity = ObjTPathologyReportHeader.ToDictionary();
                foreach (var rProperty in Hentity.Keys.ToList())
                {
                if (!Entity.Contains(rProperty))
                    Hentity.Remove(rProperty);
                }
               odal.ExecuteNonQuery("ps_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, Hentity);

               await _context.LogProcedureExecution(Hentity, "PathologyTemplate", ObjTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, UserId, UserName);

        }
        public virtual void InsertPathPrintResultentry(List<TempPathReportId> ObjTempPathReportId, int UserId, string UserName)
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
            odal.ExecuteNonQuery("ps_Delete_T_PathologyReportTemplateDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            await _context.LogProcedureExecution(tokensObj.ToDictionary(), "PathologyTemplate", tokensObj.PathReportID.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, UserName);

            string[] rEntity = { "PathReportId", "PathTemplateId", "PathTemplateDetailsResult", "TestId", "TemplateResultInHtml", "SuggestionNotes", "PathResultDr1" };
            var entity = ObjTPathologyReportTemplateDetail.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_PathologyReportTemplateDetails_1", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, "PathologyTemplate", ObjTPathologyReportTemplateDetail.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, UserName);

            string[] AEntity = { "PathReportId", "ReportDate", "ReportTime", "IsCompleted", "IsPrinted", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsTemplateTest", "SuggestionNotes","AdmVisitDoctorId","RefDoctorId"}; 
            var PathHeaderentity = ObjTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in PathHeaderentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    PathHeaderentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, PathHeaderentity);
            await _context.LogProcedureExecution(PathHeaderentity, "PathologyTemplate", ObjTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, UserId, UserName);

        }
        public virtual async Task DeleteAsync(TPathologyReportDetail ObjTPathologyReportDetail, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "PathReportId" };
            var entity = ObjTPathologyReportDetail.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_RollBack_TestForResult", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, "PathTestRollback", ObjTPathologyReportDetail.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, UserId, UserName);

        }
        public virtual async Task UpdateAsync(TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {       using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            var existing = await _context.TPathologyReportHeaders.FirstOrDefaultAsync(x => x.PathReportId == ObjTPathologyReportHeader.PathReportId);


            //  Update only the required fields
            existing.OutSourceId = ObjTPathologyReportHeader.OutSourceId;
            existing.OutSourceLabName = ObjTPathologyReportHeader.OutSourceLabName;
            existing.OutSourceSampleSentDateTime = ObjTPathologyReportHeader.OutSourceSampleSentDateTime;
            existing.OutSourceStatus = ObjTPathologyReportHeader.OutSourceStatus; 
            existing.OutSourceReportCollectedDateTime = ObjTPathologyReportHeader.OutSourceReportCollectedDateTime;
            existing.OutSourceCreatedBy = ObjTPathologyReportHeader.OutSourceCreatedBy;
            existing.OutSourceCreatedDateTime = ObjTPathologyReportHeader.OutSourceCreatedDateTime;
            existing.OutSourceModifiedby = ObjTPathologyReportHeader.OutSourceModifiedby;
            existing.OutSourceId = ObjTPathologyReportHeader.OutSourceId;
            existing.OutSourceModifiedDateTime = ObjTPathologyReportHeader.OutSourceModifiedDateTime;
            existing.ModifiedBy = ObjTPathologyReportHeader.ModifiedBy;
            existing.ModifiedDate = ObjTPathologyReportHeader.ModifiedDate;

            await _context.SaveChangesAsync();
            scope.Complete();
        }

        public virtual async Task VerifyAsync(TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPathologyReportHeader objPur = await _context.TPathologyReportHeaders.FindAsync(ObjTPathologyReportHeader.PathReportId);
                objPur.IsVerifySign = ObjTPathologyReportHeader.IsVerifySign;
                objPur.IsVerifyid = ObjTPathologyReportHeader.IsVerifyid;
                objPur.IsVerifyedDate = ObjTPathologyReportHeader.IsVerifyedDate;
                _context.TPathologyReportHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
