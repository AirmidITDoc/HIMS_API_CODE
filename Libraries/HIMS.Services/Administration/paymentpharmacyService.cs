using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            return await DatabaseHelper.GetGridDataBySp<BrowseIPDPaymentReceiptListDto>(model, "ps_Rtrv_BrowseIPDPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowseIPAdvPaymentReceiptListDto>> GetListAsync2(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPAdvPaymentReceiptListDto>(model, "ps_Rtrv_BrowseIPAdvPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowsePharmacyPayReceiptListDto>> GetListAsync3(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowsePharmacyPayReceiptListDto>(model, "ps_Rtrv_BrowsePharmacyPayReceipt");
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
        //public virtual async Task UpdateAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header & detail table records
        //        _context.PaymentPharmacies.Update(objPaymentPharmacy);
        //        _context.Entry(objPaymentPharmacy).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task UpdateAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            // Get existing entity from DB
            var existingPayment = await _context.PaymentPharmacies.FindAsync(objPaymentPharmacy.PaymentId);
            if (existingPayment == null)
                throw new Exception("Payment record not found.");

            // Copy updated values (except null ones)
            _context.Entry(existingPayment).CurrentValues.SetValues(objPaymentPharmacy);

            if (objPaymentPharmacy.UnitId == null)
                _context.Entry(existingPayment).Property(p => p.UnitId).IsModified = false;

            // Handle ignore columns (if passed)
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(existingPayment).Property(column).IsModified = false;
                }
            }

            await _context.SaveChangesAsync();
            scope.Complete();
        }
        public virtual async Task NewUpdateAsync(PaymentPharmacy objPaymentPharmacy, int type, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            if (type == 1) // PaymentPharmacy
            {
                var existing = await _context.PaymentPharmacies.FindAsync(objPaymentPharmacy.PaymentId);
                if (existing == null) throw new Exception("PaymentPharmacy record not found");

                UpdateEntity(existing, objPaymentPharmacy, ignoreColumns);
            }
            else if (type == 2) // Payment table
            {
                var existing = await _context.Payments.FindAsync(objPaymentPharmacy.PaymentId);
                if (existing == null) throw new Exception("Payment record not found");

                var mapped = new Payment();

                foreach (var prop in typeof(Payment).GetProperties())
                {
                    var sourceProp = typeof(PaymentPharmacy).GetProperty(prop.Name);

                    if (sourceProp != null)
                    {
                        var value = sourceProp.GetValue(objPaymentPharmacy);

                        if (value != null) //null skip
                            prop.SetValue(mapped, value);
                    }
                }

                UpdateEntity(existing, mapped, ignoreColumns);
            }

            await _context.SaveChangesAsync();
            scope.Complete();
        }



        private void UpdateEntity<T>(T existing, T updated, string[]? ignore = null)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == "PaymentId") continue;

                if (ignore != null && ignore.Contains(prop.Name))
                    continue;

                var val = prop.GetValue(updated);

                if (val != null)
                    prop.SetValue(existing, val);
            }
        }


        public virtual void Update(TSalesHeader ObjTSalesHeader, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] DetailEntity = { "SalesNo", "OpIpId", "OpIpType", "TotalAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "CashCounterId", "IsSellted", "IsPrint", "IsFree", "UnitId",
                "AddedBy", "UpdatedBy", "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription","ExtRegNo","CreditReason","CreditReasonId","RefundAmt","WardId","BedId","DiscperH","IsPurBill","IsBillCheck","IsRefundFlag","IsCancelled","SalesHeadName","SalesTypeId","RegId","PatientName","RegNo","ExtMobileNo","RoundOff","ExtAddress","TSalesDetails","VatAmount","ConcessionAuthorizationId"};
            var UEntity = ObjTSalesHeader.ToDictionary();
            foreach (var rProperty in DetailEntity)
            {
                UEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_PharmSalesDate", CommandType.StoredProcedure, UEntity);

        }
      
        public virtual async Task UpdateAsyncDate(PaymentPharmacy ObjPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "PaymentDate", "PaymentTime", "PaymentId" };
            var Rentity = ObjPaymentPharmacy.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_paymentpharmacy", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(PaymentPharmacy), ObjPaymentPharmacy.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

        }

        public virtual async Task GlobalDiscountUpdate(List<TSalesHeader> ObjTSalesHeader, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            foreach (var item in ObjTSalesHeader)
            {
                string[] AEntity = { "SalesId", "NetAmount", "DiscAmount", "BalanceAmount", "ConcessionReasonId", "CreatedBy" };
                var entity = item.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_Update_PhBillDiscountAfter", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TSalesHeader), item.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            }
            // Save Log
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
            // Commit Transaction
            await transaction.CommitAsync();

            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

       
        public virtual async Task<IPagedList<BrowsePharmacyTPaymentReceiptListDto>> GetTPayListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowsePharmacyTPaymentReceiptListDto>(model, "ps_Rtrv_BrowsePharmacyTPayReceipt1");
        }
    }
}
