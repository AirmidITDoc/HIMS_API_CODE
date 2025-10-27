using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.OPPatient
{
    public class OPSettlementCreditService : IOPSettlementCreditService
    {
        private readonly HIMSDbContext _context;
        public OPSettlementCreditService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Bill objBill, Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            // throw new NotImplementedException();

            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = {"OpdIpdId","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BillDate","OpdIpdType","IsCancelled","PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId",
                "IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName",
                "IsBillCheck","SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails","Payments"};
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("v_update_BillBalAmount_1", CommandType.StoredProcedure, entity);

                // Payment Code

                //objpayment.BillNo = objBill.BillNo;
                //_context.Payments.Add(objpayment);
                //await _context.SaveChangesAsync();


                string[] rPaymentEntity = { "ReceiptNo", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount", "BillNoNavigation" };

                var PaymentEntity = objpayment.ToDictionary();
                foreach (var rProperty in rPaymentEntity)
                {
                    PaymentEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("v_insert_Payment_OPIP_1", CommandType.StoredProcedure, PaymentEntity);



            }
            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }
    }
}
