using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;



namespace HIMS.Services.Pharmacy
{
    public  class PurchaseRequisitionService: IPurchaseRequisitionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PurchaseRequisitionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
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
                await _context.SaveChangesAsync();

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
                await _context.SaveChangesAsync();

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

                await _context.SaveChangesAsync();
                scope.Complete();
             }
        }
    }
}
