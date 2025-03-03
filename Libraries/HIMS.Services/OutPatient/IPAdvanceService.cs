using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class IPAdvanceService : IIPAdvanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPAdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<IPPreviousBillListDto>> GetIPPreviousBillAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPreviousBillListDto>(model, "Rtrv_IPPreviousBill_info");
        }
        public virtual async Task<IPagedList<IPAddchargesListDto>> GetIPAddchargesAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAddchargesListDto>(model, "ps_Rtrv_AddChargesList");
        }
        public virtual async Task<IPagedList<IPBillList>> GetIPBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillList>(model, "m_Rtrv_IP_Bill_List_Settlement");
        }
        public virtual async Task InsertAsync(AddCharge objAddCharge, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.AddCharges.Add(objAddCharge);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var entity = objPayment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            entity["OPDIPDType"] = 1; // Ensure objpayment has OPDIPDType
            string PaymentId = odal.ExecuteNonQuery("ps_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);


            string[] rDetailEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt","BillDate", "OpdIpdType", "IsCancelled",
                                              "PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled",
                                             "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck",
                                              "SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails"};

            var BillEntity = ObjBill.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                BillEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_BillBalAmount_1", CommandType.StoredProcedure, BillEntity);

            foreach (var item in objadvanceDetailList)
            {

                string[] ADetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };

                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in ADetailEntity)
                {
                    AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);

            }


            string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var AdvanceHeaderEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in AHeaderEntity)
            {
                AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);


            await _context.SaveChangesAsync(UserId, UserName);
        }




    
        
    }
}
