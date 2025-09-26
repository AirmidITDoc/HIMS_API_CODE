using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


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
        public virtual async Task RadiologyUpdate(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] REntity = { "RadDate", "RadTime", "OpdIpdType", "OpdIpdId", "RadTestId", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "TestType" };
            var Dentity = ObjTRadiologyReportHeader.ToDictionary();
            foreach (var rProperty in REntity)
            {
                Dentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_T_RadiologyReportHeader_1", CommandType.StoredProcedure, Dentity);
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
                objPur.IsVerified = ObjTRadiologyReportHeader.IsVerified;
                objPur.IsVerifyedDate = ObjTRadiologyReportHeader.IsVerifyedDate;
                _context.TRadiologyReportHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
