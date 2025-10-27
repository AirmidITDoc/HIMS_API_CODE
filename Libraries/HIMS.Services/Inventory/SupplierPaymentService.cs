using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class SupplierPaymentService : ISupplierPaymentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SupplierPaymentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TGrnheader objGRN, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TGrnheaders.Add(objGRN);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task InsertAsync(TGrnheader objGRN, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // remove conditional records
        //        if (objGRN.IsPaymentProcess == true)
        //            objGRN.TSupPayDets = null;
        //        else
        //            objGRN.TSupPayDets = null;
        //        // Add header table records
        //        _context.TGrnheaders.Add(objGRN);
        //        await _context.SaveChangesAsync();
        //        scope.Complete();
        //    }
        //}

        public Task InsertAsyncLoop(TGrnheader objGRN, int UserId, string Username)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(TGrnheader objGRN, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TGrnheaders.Update(objGRN);
                _context.Entry(objGRN).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }

}

