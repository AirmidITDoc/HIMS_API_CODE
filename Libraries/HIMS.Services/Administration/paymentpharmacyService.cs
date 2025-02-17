using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class paymentpharmacyService : IPaymentpharmacyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public paymentpharmacyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<BrowseOPDPaymentReceiptListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseOPDPaymentReceiptListDto>(model, "Retrieve_BrowseOPDPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowseIPDPaymentReceiptListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPDPaymentReceiptListDto>(model, "Retrieve_BrowseIPDPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowseIPAdvPaymentReceiptListDto>> GetListAsync2(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPAdvPaymentReceiptListDto>(model, "Retrieve_BrowseIPAdvPaymentReceipt");
        }
        public virtual async Task InsertAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.PaymentPharmacies.Add(objPaymentPharmacy);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.PaymentPharmacies.Update(objPaymentPharmacy);
                _context.Entry(objPaymentPharmacy).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task CancelAsync(PaymentPharmacy objPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header table records
        //        PaymentPharmacy objsup = await _context.MSupplierMasters.FindAsync(objPaymentPharmacy.SupplierId);
        //        _context.PaymentPharmacies.Update(objsup);
        //        _context.Entry(objsup).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
    }
}
 