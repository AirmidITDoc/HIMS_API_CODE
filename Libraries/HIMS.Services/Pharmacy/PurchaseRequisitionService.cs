using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace HIMS.Services.Pharmacy
{
    public  class PurchaseRequisitionService: IPurchaseRequisitionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PurchaseRequisitionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<TPurchaseRequisitionHeader> GetById(int Id)
        {
            return await this._context.TPurchaseRequisitionHeaders.Include(x => x.TPurchaseRequisitionDetails).FirstOrDefaultAsync(x => x.PurchaseRequisitionId == Id);
        }
        public virtual async Task<IPagedList<PurchaseRequitionListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseRequitionListDto>(model, "ps_PurchaseRequisitionHeaderList");
        }
        public virtual async Task<IPagedList<PurRequisiionItemListDto>> GetListItemAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurRequisiionItemListDto>(model, "ps_rtrv_PurRequisiionItemList");
        }
        public virtual async Task<IPagedList<PurchaseRequisitionDetailListDto>> GetListAsyncPurchase(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseRequisitionDetailListDto>(model, "ps_PurchaseRequisitionDetailsList");
        }
        public virtual async Task InsertAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TPurchaseRequisitionHeaders
                    .OrderByDescending(x => x.PurchaseRequisitionNo)
                    .Select(x => x.PurchaseRequisitionNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTPurchaseRequisitionHeader.PurchaseRequisitionNo = newSeqNo.ToString();


                ObjTPurchaseRequisitionHeader.CreatedBy = UserId;
                ObjTPurchaseRequisitionHeader.CreatedDate = AppTime.Now;

                _context.TPurchaseRequisitionHeaders.Add(ObjTPurchaseRequisitionHeader);
                await _context.SaveChangesAsync(UserId, Username);

                scope.Complete();
            }
        }
      

        public virtual async Task UpdateAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
             {
                long PurchaseRequisitionId = ObjTPurchaseRequisitionHeader.PurchaseRequisitionId;

                // ✅ Delete related details first
                var lstAttend = await _context.TPurchaseRequisitionDetails
                    .Where(x => x.PurchaseRequisitionId == PurchaseRequisitionId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TPurchaseRequisitionDetails.RemoveRange(lstAttend);


                // Save deletion first
                await _context.SaveChangesAsync(UserId, Username);

                // Then attach and update header
                _context.Attach(ObjTPurchaseRequisitionHeader);
                _context.Entry(ObjTPurchaseRequisitionHeader).State = EntityState.Modified;

                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.PurchaseRequisitionNo).IsModified = false;

                ObjTPurchaseRequisitionHeader.ModifiedBy = UserId;
                ObjTPurchaseRequisitionHeader.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTPurchaseRequisitionHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync(UserId, Username);
                scope.Complete();
             }
        }


        public virtual async Task Cancel(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // OLD CODE With SP
                DatabaseHelper odal = new();

                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                                                                         //throw new NotImplementedException();
                string[] Entity = { "PurchaseRequisitionId", "IsCancelledBy" };
                var entity = ObjTPurchaseRequisitionHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("PS_Cancel_PurchaseRequisition", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity.ToDictionary(), nameof(MCompanyWiseServiceDiscount), (int?)ObjTPurchaseRequisitionHeader.PurchaseRequisitionId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                // Save & Commit
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task VerifyAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPurchaseRequisitionHeader objPur = await _context.TPurchaseRequisitionHeaders.FindAsync(ObjTPurchaseRequisitionHeader.PurchaseRequisitionId);
                objPur.Isverify = ObjTPurchaseRequisitionHeader.Isverify;
                objPur.IsInchargeVerifyId = ObjTPurchaseRequisitionHeader.IsInchargeVerifyId;
                objPur.IsInchargeVerifyDate = ObjTPurchaseRequisitionHeader.IsInchargeVerifyDate;
                _context.TPurchaseRequisitionHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync(UserId, Username);

                scope.Complete();
            }
        }

    }
}
