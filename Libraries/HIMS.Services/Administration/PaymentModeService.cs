using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly HIMSDbContext _context;
        public PaymentModeService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username)
        {
            // throw new NotImplementedException();
            _context.Payments.Update(objPayment);
            _context.Entry(objPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //scope.Complete();
        }
        public virtual async Task<IPagedList<OPBillListForPaymentModeChangeListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListForPaymentModeChangeListDto>(model, "ps_rtrv_OPBillListForPaymentModeChange");
        }

        public virtual async Task<IPagedList<OPBillListForPaymentModeChangeListBillNoWiseDto>> GetBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListForPaymentModeChangeListBillNoWiseDto>(model, "ps_rtrv_PaymentByBillNoWise");
        }

        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            // Get existing entity from DB
            var existingPayment = await _context.Payments.FindAsync(objPayment.PaymentId);
            if (existingPayment == null)
                throw new Exception("Payment record not found.");

            // Copy updated values (except null ones)
            _context.Entry(existingPayment).CurrentValues.SetValues(objPayment);

            if (objPayment.UnitId == null)
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
        public virtual async Task PaymentUpdateAsync(List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            foreach (var item in ObjTPayment)
            {

                string[] AEntity = { "PaymentId", "BillNo", "PayMode", "TranNo", "BankName" };
                var Rentity = item.ToDictionary();

                foreach (var rProperty in Rentity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        Rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_PaymentMode", CommandType.StoredProcedure, Rentity);
                await _context.LogProcedureExecution(Rentity, nameof(TPayment), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            }
        }
        public virtual async Task NewPaymentUpdateAsync(List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            foreach (var item in ObjTPayment)
            {

                string[] AEntity = { "PaymentId", "UnitId", "BillNo", "Opdipdtype", "ReceiptNo", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount", "Comments", "PayMode", "OnlineTranNo", "OnlineTranResponse", "CompanyId", "AdvanceId", "RefundId", "CashCounterId", "TransactionType", "TransactionLabel", "IsSelfOrcompany", "TranMode", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "CreatedBy" };
                var Rentity = item.ToDictionary();

                foreach (var rProperty in Rentity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        Rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_NewSaveUpdate_Payment", CommandType.StoredProcedure, Rentity);
                await _context.LogProcedureExecution(Rentity, nameof(TPayment), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            }
        }
        public virtual async Task NewUpdateAsync(PaymentPharmacy objPaymentPharmacy, int type, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

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

        public virtual async Task PaymentPharmacyUpdateAsync(List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            foreach (var item in ObjTPaymentPharmacy)
            {

                string[] AEntity = { "PaymentId", "BillNo", "PayMode", "TranNo", "BankName" };
                var Rentity = item.ToDictionary();

                foreach (var rProperty in Rentity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        Rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_PaymentPharmacyMode", CommandType.StoredProcedure, Rentity);
                await _context.LogProcedureExecution(Rentity, nameof(TPayment), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            }
        }
        public virtual async Task Cancel(TPayment ObjTPayment, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "PaymentId", "IsCancelledBy" };
            var CAdvanceEntity = ObjTPayment.ToDictionary();
            foreach (var rProperty in CAdvanceEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    CAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_T_Payment", CommandType.StoredProcedure, CAdvanceEntity);
        }

    }
}


