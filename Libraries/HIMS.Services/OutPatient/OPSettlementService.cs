using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace HIMS.Services.OutPatient
{
    public class OPSettlementService : IOPSettlementService
    {
        private readonly HIMSDbContext _context;
        public OPSettlementService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OPBillListSettlementListDto>> OPBillListSettlementList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListSettlementListDto>(objGrid, "m_Rtrv_OP_Bill_List_Settlement");
        }

        public virtual async Task InsertAsyncSP(Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rpayEntity = { "ReceiptNo", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount",
                "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount",};
            var payentity = objpayment.ToDictionary();
            foreach (var rProperty in rpayEntity)
            {
                payentity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("v_insert_Payment_OPIP_1", CommandType.StoredProcedure, "PaymentId", payentity);
            objpayment.PaymentId = Convert.ToInt32(PaymentId);
        }
        public virtual async Task InsertAsync(Payment objpayment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.Payments.Add(objpayment);
                await _context.SaveChangesAsync();
                
                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(Bill objBill, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.Bills.Update(objBill);
                _context.Entry(objBill).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task UpdateAsyncSP(Bill objBill, int currentUserId, string currentUserName)
        //{

        //    //throw new NotImplementedException();
        //    DatabaseHelper odal = new();
        //    string[] rAdmissEntity = {"OpdIpdId","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt ","BalanceAmt","",
        //    "BillDate","OpdIpdType","IsCancelled","PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled","IsPrinted",
        //        "IsFree", "CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck","SpeTaxPer","SpeTaxAmt","IsBillShrHold",
        //        "DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails"};
        //    var rAdmissentity1 = objBill.ToDictionary();
        //    foreach (var rProperty in rAdmissEntity)
        //    {
        //        rAdmissentity1.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("m_update_BillBalAmount_1", CommandType.StoredProcedure, rAdmissentity1);
        //    // objAdmission.AdmissionId = Convert.ToInt32(objAdmission.AdmissionId);
        //}
    }

}
