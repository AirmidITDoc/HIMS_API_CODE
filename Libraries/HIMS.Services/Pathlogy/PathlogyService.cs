using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
       
        public List<pathologistdoctorDto> SearchPatient()
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = Array.Empty<SqlParameter>(); 
            var data = sql.FetchListBySP<pathologistdoctorDto>("Retrieve_PathologistDoctorMasterForCombo", para);
            return data;
        }

        public virtual async Task InsertAsyncResultEntry(List<TPathologyReportDetail> ObjPathologyReportDetail, TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                foreach (var item in ObjPathologyReportDetail)
                {
                    var tokensObj = new
                    {
                        PathReportID = Convert.ToInt32(item.PathReportId)
                    };

                    odal.ExecuteNonQuery("ps_Delete_T_PathologyReportDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
                    await _context.LogProcedureExecution( tokensObj.ToDictionary(),  "ps_Delete_T_PathologyReportDetails",  item.PathReportId.ToInt(),  Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
                }

                foreach (var item in ObjPathologyReportDetail)
                {

                    string[] rEntity = { "PathReportId", "CategoryId", "TestId", "SubTestId", "ParameterId", "ResultValue", "UnitId", "NormalRange", "PrintOrder", "PisNumeric", "Opdipdid", "Opdipdtype", "CategoryName", "TestName", "SubTestName", "ParameterName", "UnitName", "PatientName", "RegNo", "SampleId", "ParaBoldFlag", "MinValue", "MaxValue", "Opipnumber", "AgeY", "AgeM", "AgeD", "GenderId", "SampleNo", "SuggestionNotes" };
                    var entity = item.ToDictionary();

                    foreach (var rProperty in entity.Keys.ToList())
                    {
                        if (!rEntity.Contains(rProperty))
                            entity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("ps_insert_PathRrptDet_1", CommandType.StoredProcedure, entity);
                    await _context.LogProcedureExecution(entity, nameof(TPathologyReportDetail), item.PathReportDetId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                }

                string[] Entity = { "PathReportId", "ReportDate", "ReportTime", "IsCompleted", "IsPrinted", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsTemplateTest", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "AddedBy" };
                var Hentity = ObjTPathologyReportHeader.ToDictionary();
                foreach (var rProperty in Hentity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, Hentity);
                await _context.LogProcedureExecution(Hentity, nameof(TPathologyReportHeader), ObjTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                // ✅ Commit
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }

        }
            
        public virtual async Task InsertPathPrintResultentry(List<TempPathReportId> ObjTempPathReportId, int CurrentUserId, string CurrentUserName)
        {
             await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            odal.ExecuteNonQuery("m_truncate_Temp_PathReportId", CommandType.StoredProcedure);
            await _context.LogProcedureExecution( new Dictionary<string, object>(), "m_truncate_Temp_PathReportId", 0, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

            foreach (var item in ObjTempPathReportId)
            {
                var entity = item.ToDictionary();

                odal.ExecuteNonQuery("m_Insert_Temp_PathReportId", CommandType.StoredProcedure, entity);

                await _context.LogProcedureExecution( entity,nameof(TempPathReportId),item.PathReportId.ToInt(),Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                // ✅ Commit
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertAsyncResultEntry1(TPathologyReportTemplateDetail ObjTPathologyReportTemplateDetail, TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            var tokensObj = new
            {
                PathReportID = Convert.ToInt32(ObjTPathologyReportTemplateDetail.PathReportId)
            };
            odal.ExecuteNonQuery("ps_Delete_T_PathologyReportTemplateDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            await _context.LogProcedureExecution(tokensObj.ToDictionary(), "PathologyTemplate", tokensObj.PathReportID.ToInt(), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

            string[] rEntity = { "PathReportId", "PathTemplateId", "PathTemplateDetailsResult", "TestId", "TemplateResultInHtml", "SuggestionNotes", "PathResultDr1" };
            var entity = ObjTPathologyReportTemplateDetail.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_PathologyReportTemplateDetails_1", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TPathologyReportTemplateDetail), ObjTPathologyReportTemplateDetail.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                string[] AEntity = { "PathReportId", "ReportDate", "ReportTime", "IsCompleted", "IsPrinted", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsTemplateTest", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "AddedBy" };
            var PathHeaderentity = ObjTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in PathHeaderentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    PathHeaderentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_PathologyReportHeader_1", CommandType.StoredProcedure, PathHeaderentity);
            await _context.LogProcedureExecution(PathHeaderentity, nameof(TPathologyReportHeader), ObjTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

            // ✅ Commit
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task DeleteAsync(TPathologyReportDetail ObjTPathologyReportDetail, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            string[] AEntity = { "PathReportId" };
            var entity = ObjTPathologyReportDetail.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_RollBack_TestForResult", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, "PathTestRollback", ObjTPathologyReportDetail.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

            // ✅ Commit
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task UpdateAsync(TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

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



        List<PathServicewiseTemplateListDto> IPathlogyService.GetServicewisetemplate(int ServiceId)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@ServiceId", ServiceId);

            List<PathServicewiseTemplateListDto> lstServiceList = sql.FetchListBySP<PathServicewiseTemplateListDto>("rtrv_patientTemplateRetriveCombo", para);
            return lstServiceList;
        }
        public virtual async Task UnVerifyAsyncSp(TPathologyReportHeader ObjTPathologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
            string[] rEntity = { "PathReportId", "UnVerifyId", "UnVerifyComment", "UnVerifyDateTime" };
            var entity = ObjTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_PathologyReportHeaderUnverify", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TPathologyReportHeader), ObjTPathologyReportHeader.PathReportId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
             // ✅ Commit
             await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }

      
    }
}
