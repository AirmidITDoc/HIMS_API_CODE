using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;


namespace HIMS.Services.Radiology
{
    public class RadiologyService : IRadilogyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RadiologyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<RadiologyListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RadiologyListDto>(model, "m_Rtrv_RadilogyResultEntryList_Ptnt_Dtls");
        }

        public virtual async Task<IPagedList<LabRadiologyListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabRadiologyListDto>(model, "m_Rtrv_LabRadiologyResultEntryList_Ptnt_Dtls");
        }

        public virtual async Task<IPagedList<RadiologyApproveListDto>> ListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RadiologyApproveListDto>(model, "ps_Rtrv_LabRadiologyResult_Completed_List");
        }
        public virtual async Task RadiologyUpdate(TRadiologyReportHeader ObjTRadiologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] REntity = { "RadReportId", "ReportDate", "ReportTime", "IsCompleted", "IsPrinted", "RadResultDr1", "RadResultDr2", "RadResultDr3", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "ResultEntry" };
            var Dentity = ObjTRadiologyReportHeader.ToDictionary();
            foreach (var rProperty in Dentity.Keys.ToList())
            {
                if (!REntity.Contains(rProperty))
                    Dentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_T_RadiologyReportHeader_1", CommandType.StoredProcedure, Dentity);
            await _context.LogProcedureExecution(Dentity, nameof(TRadiologyReportHeader), Convert.ToInt32(ObjTRadiologyReportHeader.RadReportId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
        }
        public virtual async Task UpdateAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            var existing = await _context.TRadiologyReportHeaders.FirstOrDefaultAsync(x => x.RadReportId == ObjTRadiologyReportHeader.RadReportId);


            //Update only the required fields
            existing.OutSourceId = ObjTRadiologyReportHeader.OutSourceId;
            existing.OutSourceLabName = ObjTRadiologyReportHeader.OutSourceLabName;
            existing.OutSourceSampleSentDateTime = ObjTRadiologyReportHeader.OutSourceSampleSentDateTime;
            existing.OutSourceStatus = ObjTRadiologyReportHeader.OutSourceStatus;
            existing.OutSourceReportCollectedDateTime = ObjTRadiologyReportHeader.OutSourceReportCollectedDateTime;
            existing.OutSourceCreatedBy = ObjTRadiologyReportHeader.OutSourceCreatedBy;
            existing.OutSourceCreatedDateTime = ObjTRadiologyReportHeader.OutSourceCreatedDateTime;
            existing.OutSourceModifiedby = ObjTRadiologyReportHeader.OutSourceModifiedby;
            existing.OutSourceId = ObjTRadiologyReportHeader.OutSourceId;
            existing.OutSourceModifiedDateTime = ObjTRadiologyReportHeader.OutSourceModifiedDateTime;
            existing.ModifiedBy = ObjTRadiologyReportHeader.ModifiedBy;
            existing.ModifiedDate = ObjTRadiologyReportHeader.ModifiedDate;

            await _context.SaveChangesAsync();
            scope.Complete();
        }
        public virtual async Task VerifyAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TRadiologyReportHeader objPur = await _context.TRadiologyReportHeaders.FindAsync(ObjTRadiologyReportHeader.RadReportId);
                objPur.IsVerifyId = ObjTRadiologyReportHeader.IsVerifyId;
             //   objPur.IsVerified = ObjTRadiologyReportHeader.IsVerified;
                objPur.IsVerifySign = ObjTRadiologyReportHeader.IsVerifySign;
                objPur.IsVerifyedDate = ObjTRadiologyReportHeader.IsVerifyedDate;
                _context.TRadiologyReportHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
