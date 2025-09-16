using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
using System.Transactions;

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
            return await DatabaseHelper.GetGridDataBySp<OPBillListSettlementListDto>(objGrid, "ps_Rtrv_OP_Bill_List_Settlement");
        }

        public virtual async Task InsertAsyncSP(Payment objpayment, Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            // Insert In Payment 
            string[] rpayEntity = { "ReceiptNo", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount",
                                    "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate" };
            var payentity = objpayment.ToDictionary();
            foreach (var rProperty in rpayEntity)
            {
                payentity.Remove(rProperty);
            }
            // Add the new parameter
            payentity["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
            string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", payentity);
            objpayment.PaymentId = Convert.ToInt32(PaymentId);

            //Udpate Bill Table 
            string[] rBillEntity = {"OpdIpdId","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt",
                                    "BillDate","OpdIpdType","IsCancelled","PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled","IsPrinted",
                                    "IsFree", "CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck","SpeTaxPer","SpeTaxAmt","IsBillShrHold",
                                    "DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails",
            "RegNo","PatientName","Ipdno","AgeYear","AgeMonth","AgeDays","DoctorId","DoctorName","WardId","BedId","PatientType","CompanyName","CompanyAmt","PatientAmt","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",
};
            var rAdmissentity1 = objBill.ToDictionary();
            foreach (var rProperty in rBillEntity)
            {
                rAdmissentity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, rAdmissentity1);

            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
        }
        public virtual async Task InsertAsync(Payment objpayment, Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                DatabaseHelper odal = new();

                _context.Payments.Add(objpayment);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }

}
