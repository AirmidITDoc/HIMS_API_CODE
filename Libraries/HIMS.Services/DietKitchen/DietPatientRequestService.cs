using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{
    public  class DietPatientRequestService : IDietPatientRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietPatientRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DietPatientRequestHeaderListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPatientRequestHeaderListDto>(model, "ps_Rtrv_DietPatientRequestHeader");
        }
        public virtual async Task<IPagedList<DietPatientRequestDetailsListDto>> GetListDetailsAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPatientRequestDetailsListDto>(model, "ps_Rtrv_DietPatReqDetails");
        }
        public virtual async Task InsertAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TDietPatientRequestHeaders
                    .OrderByDescending(x => x.DietReqNo)
                    .Select(x => x.DietReqNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTDietPatientRequestHeader.DietReqNo = newSeqNo.ToString();


                ObjTDietPatientRequestHeader.CreatedBy = UserId;
                ObjTDietPatientRequestHeader.CreatedDate = AppTime.Now;

                _context.TDietPatientRequestHeaders.Add(ObjTDietPatientRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope( TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            long dietReqId = ObjTDietPatientRequestHeader.DietReqId;

            var newDetails = ObjTDietPatientRequestHeader.TDietPatReqDetails?.ToList() ?? new List<TDietPatReqDetail>(); ObjTDietPatientRequestHeader.TDietPatReqDetails = null;

            // Delete existing related details first
            var lstDetails = await _context.TDietPatReqDetails  .Where(x => x.DietReqId == dietReqId)  .ToListAsync();

            if (lstDetails.Any()) _context.TDietPatReqDetails.RemoveRange(lstDetails);

            // Save deletion first
            await _context.SaveChangesAsync();

            // Then attach and update header
            _context.Attach(ObjTDietPatientRequestHeader);
            _context.Entry(ObjTDietPatientRequestHeader).State = EntityState.Modified;

            _context.Entry(ObjTDietPatientRequestHeader).Property(x => x.DietReqNo).IsModified = false;

            ObjTDietPatientRequestHeader.ModifiedBy = UserId;
            ObjTDietPatientRequestHeader.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(ObjTDietPatientRequestHeader).Property(column).IsModified = false;
            }

            // Re-insert the (new) detail rows against this header
            foreach (var detail in newDetails)
            {
                detail.DietReqId = dietReqId;
            }
            await _context.TDietPatReqDetails.AddRangeAsync(newDetails);

            await _context.SaveChangesAsync();
            scope.Complete();
        }
        public virtual async Task Cancel(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                                                                         //throw new NotImplementedException();
            string[] Entity = { "DietReqId", "IsCancelledBy", "CancelledReason" };
            var entity = ObjTDietPatientRequestHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQueryNew("PS_DietPatientRequestDelete", CommandType.StoredProcedure,"", entity);
            await _context.LogProcedureExecution(entity, nameof(TDietPatientRequestHeader), (int)ObjTDietPatientRequestHeader.DietReqId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
            // Save audit log changes
            await _context.SaveChangesAsync();

            // Commit transaction
             await transaction.CommitAsync();
            }
            catch
            {
            // Rollback transaction on error
            await transaction.RollbackAsync();
            throw;
            }

        }
        public virtual async Task CancelD(TDietPatReqDetail ObjTDietPatReqDetail, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity = { "DietReqDetId", "IsCancelledBy", "CancelledReason" };
                var entity = ObjTDietPatReqDetail.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("PS_DietPatReqDetailsDelete", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(TDietPatReqDetail), (int)ObjTDietPatReqDetail.DietReqDetId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                // Save audit log changes
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback transaction on error
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task AcceptD(TDietPatReqDetail ObjTDietPatReqDetail, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity = { "DietReqDetId", "IsAccept", "IsAcceptedBy" };
                var entity = ObjTDietPatReqDetail.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("PS_DietPatReqDetailsAccept", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(TDietPatReqDetail), (int)ObjTDietPatReqDetail.DietReqDetId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                // Save audit log changes
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback transaction on error
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
