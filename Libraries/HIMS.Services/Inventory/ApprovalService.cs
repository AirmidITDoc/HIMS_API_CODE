using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
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

namespace HIMS.Services.Inventory
{
    public class ApprovalService : IApprovalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ApprovalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ApprovalListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ApprovalListDto>(model, "ps_ApprovalHeaderList");
        }
        public virtual async Task<IPagedList<UserApprovalNamelistDto>> NewGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UserApprovalNamelistDto>(model, "ps_Rtrv_UserApprovalNamelist");
        }
        public virtual async Task InsertAsync(TApprovalHeader ObjTApprovalHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TApprovalHeaders
                    .OrderByDescending(x => x.ApprovalNo)
                    .Select(x => x.ApprovalNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTApprovalHeader.ApprovalNo = newSeqNo.ToString();

                ObjTApprovalHeader.CreatedBy = UserId;
                ObjTApprovalHeader.CreatedDate = AppTime.Now;

                _context.TApprovalHeaders.Add(ObjTApprovalHeader);
                await _context.SaveChangesAsync();

                // Update Purchase Header
                var purchaseHeader = await _context.TPurchaseHeaders
                    .FirstOrDefaultAsync(x => x.PurchaseId == ObjTApprovalHeader.TranId);

                if (purchaseHeader != null)
                {
                    purchaseHeader.IsProceedToApproval = true;
                    // Optional:
                    // purchaseHeader.ModifiedBy = UserId;
                    // purchaseHeader.ModifiedDate = AppTime.Now;

                    await _context.SaveChangesAsync();
                }


                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TApprovalHeader objApproval, int currentUserId, string currentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());// <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());    // <-- Share same DbTransaction

                string[] Entity ={ "ApprovalId", "ApprovalStatus", "ApprovedDateTime" };

                var entity = objApproval.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew( "ps_UpdateApprovalStatus", CommandType.StoredProcedure, "", entity);

                await _context.LogProcedureExecution(entity, nameof(TApprovalHeader), objApproval.ApprovalId.ToInt(),  Core.Domain.Logging.LogAction.Edit, currentUserId, currentUserName);

                await _context.SaveChangesAsync(currentUserId, currentUserName);

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
