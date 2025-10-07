using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.SqlQuery.SqlPredicate;

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
        public virtual async Task UpdateAsync(TSalesHeader ObjTSalesHeader, int UserId, string UserName)
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
        //public virtual async Task UpdateAsyncDate(PaymentPharmacy ObjPaymentPharmacy, int UserId, string UserName)
        //{
        //    //throw new NotImplementedException();
        //    DatabaseHelper odal = new();
        //    string[] DetailEntity = { "BillNo", "ReceiptNo", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId",
        //        "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate","CashCounterId","IsSelfOrcompany","NeftpayAmount","Neftno","NeftbankMaster","Neftdate","PayTmamount","PayTmtranNo","PayTmdate","StrId","TranMode","CompanyId"};
        //    var UEntity = ObjPaymentPharmacy.ToDictionary();
        //    foreach (var rProperty in DetailEntity)
        //    {
        //        UEntity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("ps_paymentpharmacy", CommandType.StoredProcedure, UEntity);

        //}
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

    }
}
 