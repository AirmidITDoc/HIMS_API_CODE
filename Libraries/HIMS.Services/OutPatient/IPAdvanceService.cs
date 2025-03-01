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
    public class IPAdvanceService:IIPAdvanceService
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

        public virtual async Task paymentAsyncSP(Payment objPayment, Bill ObjBill,List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int UserId, string UserName)
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




        //string[] rDetailEntity = { "AdvanceNo", "Advance" };

        //var AdvanceEntity = objAdvanceDetail.ToDictionary();
        //foreach (var rProperty in rDetailEntity)
        //{
        //    AdvanceEntity.Remove(rProperty);
        //}
        //string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);
        //objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);


        //string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
        //var PAdvanceEntity = objPayment.ToDictionary();
        //foreach (var rProperty in PayEntity)
        //{
        //    PAdvanceEntity.Remove(rProperty);
        //}
        //odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);


        public virtual async Task InsertAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail advanceDetail, Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            try
            {
              
                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {

                    //AdHeader
                    ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));

                    _context.AdvanceHeaders.Add(objAdvanceHeader);
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                       //await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                    advanceDetail.AdvanceId = objAdvanceHeader.AdvanceId;
                    _context.AdvanceDetails.Add(advanceDetail);
                    //_context.ConfigSettings.Update(objConfigRSetting);
                    //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                      //await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                    objpayment.AdvanceId = objAdvanceHeader.AdvanceId;
                    //_context.ConfigSettings.Update(objConfigRSetting);
                    //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                    _context.Payments.Add(objpayment);

                    await _context.SaveChangesAsync();


                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                AdvanceHeader? objAdvanceHeader1 = await _context.AdvanceHeaders.FindAsync(objAdvanceHeader.AdvanceId);
                _context.AdvanceHeaders.Remove(objAdvanceHeader1);
                await _context.SaveChangesAsync();
            }
        }
    }
}
