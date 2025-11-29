using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
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
        public virtual async Task InsertAsyncSP(Payment objpayment, Bill objBill, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)

        {
            DatabaseHelper odal = new();
            string[] rpayEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "OPDIPDType", "PaymentId", "CompanyId" };

            var payentity = objpayment.ToDictionary();
            foreach (var rProperty in payentity.Keys.ToList())
            {
                if (!rpayEntity.Contains(rProperty))
                    payentity.Remove(rProperty);
            }
            // Add the new parameter
            payentity["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType

            string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", payentity);

            objpayment.PaymentId = Convert.ToInt32(PaymentId);

            //Udpate Bill Table 

            string[] rBillEntity = { "BillNo", "BalanceAmt" };


            var rAdmissentity1 = objBill.ToDictionary();

            foreach (var rProperty in rAdmissentity1.Keys.ToList())
            {
                if (!rBillEntity.Contains(rProperty))
                    rAdmissentity1.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, rAdmissentity1);

            foreach (var item in ObjTPayment)
            {
                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
            }

            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

        }


        public virtual async Task InsertSettlementMultiple(List<Payment> objpayment, List<Bill> objBill, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            // Insert In Payment 
            foreach (var items in objpayment)
            {

                string[] rpayEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "OPDIPDType", "PaymentId", "CompanyId" };
                var payentity = items.ToDictionary();
                foreach (var rProperty in payentity.Keys.ToList())
                {
                    if (!rpayEntity.Contains(rProperty))
                        payentity.Remove(rProperty);
                }
                // Add the new parameter
                payentity["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
                string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", payentity);
                items.PaymentId = Convert.ToInt32(PaymentId);
                await _context.LogProcedureExecution(payentity, nameof(Payment), items.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            }

            //Udpate Bill Table
            foreach (var items in objBill)
            {

                string[] rBillEntity = { "BillNo", "BalanceAmt" };
                var rAdmissentity1 = items.ToDictionary();
                foreach (var rProperty in rAdmissentity1.Keys.ToList())
                {
                    if (!rBillEntity.Contains(rProperty))
                        rAdmissentity1.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, rAdmissentity1);
                await _context.LogProcedureExecution(rAdmissentity1, nameof(Bill), items.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


            }
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
