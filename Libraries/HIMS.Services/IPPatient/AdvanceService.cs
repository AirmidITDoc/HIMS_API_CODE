using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.Services.IPPatient
{
    public class AdvanceService : IAdvanceService
    {
        private readonly HIMSDbContext _context;
        public AdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<AdvanceDetailListDto>> AdvanceDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdvanceDetailListDto>(model, "Rtrv_T_AdvanceList");
        }

        public virtual async Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdvanceListDto>(model, "m_Rtrv_BrowseIPAdvanceList");
        }
        public virtual async Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundOfAdvanceListDto>(model, "m_Rtrv_BrowseIPRefundAdvanceReceipt");
        }

        //SHILPA CODE////
        public virtual async Task InsertAdvanceAsyncSP1(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment ,int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "AdvanceDetails" };
            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vAdvanceId = odal.ExecuteNonQuery("sp_insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(vAdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(vAdvanceId);

            string[] rDetailEntity = { "AdvanceNo", "Advance" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);

            string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity)
            {
                PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);

        }









         public virtual async Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objpayment, int UserId , string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "AdvanceDetails" };

            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string AdvanceId = odal.ExecuteNonQuery("v_insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(AdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(AdvanceId);
         

            string[] rDetailEntity = { "AdvanceNo", "Advance" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("v_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);
            objpayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);

            string[] rPaymentEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "BillNoNavigation" };

            //var PaymentEntity = objpayment.ToDictionary();
            //foreach (var rProperty in rPaymentEntity)
            //{
            //    PaymentEntity.Remove(rProperty);
            //}
            //odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure,  PaymentEntity);

            // Payment Code
            int _val = 0;
            _context.Payments.Add(objpayment);
            await _context.SaveChangesAsync();

        }


        public virtual async Task InsertAsyncSP(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] rRefundEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var refundentity = objRefund.ToDictionary();


            // Define output parameter for RefundId
            var RefundId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Direction = ParameterDirection.Output
            };

            foreach (var rProperty in rRefundEntity)
            {
                refundentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_insert_IPAdvRefund_1", CommandType.StoredProcedure, "RefundId", refundentity);

            // Retrieve the output RefundId
             string refundId = RefundId.Value.ToString();


            string[] rEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_AdvanceHeader_1", CommandType.StoredProcedure, entity);




            string[] rDetailEntity = { "IsCancelled", "IsCancelledby", "IsCancelledDate" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_AdvRefundDetail_1", CommandType.StoredProcedure, AdvanceEntity);


            string[] rBalEntity = { "IsCancelled", "IsCancelledby", "IsCancelledDate" };

            var BalEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rBalEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, BalEntity);




            string[] rPaymentEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate" };

            var PaymentEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPaymentEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_m_insert_Payment_1", CommandType.StoredProcedure, PaymentEntity);

            objRefund.RefundId = Convert.ToInt32(RefundId);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(RefundId);
            objAdvRefundDetail.AdvRefId = Convert.ToInt32(RefundId);
            objAdvanceDetail.AdvanceDetailId = Convert.ToInt32(RefundId);
            objPayment.PaymentId = Convert.ToInt32(RefundId);

        }

        public virtual async Task UpdateAdvanceAsyncSP(AdvanceDetail objAdvanceDetail,int UserId, string UserName)
        {
         

            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType","BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };


            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_Update_Advance_det", CommandType.StoredProcedure,AdvanceEntity);
           // objpayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);

            //string[] rPaymentEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "BillNoNavigation" };

            //var PaymentEntity = objpayment.ToDictionary();
            //foreach (var rProperty in rPaymentEntity)
            //{
            //    PaymentEntity.Remove(rProperty);
            //}
            //odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure, PaymentEntity);
        }

    }
    //    {
    //        DatabaseHelper odal = new();

    //        // Prepare Refund entity
    //        var refundEntity = objRefund.ToDictionary();
    //        RemoveUnnecessaryProperties(refundEntity, new[] { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" });

    //        // Define output parameter for RefundId
    //        var RefundId = new SqlParameter
    //        {
    //            SqlDbType = SqlDbType.BigInt,
    //            ParameterName = "@RefundId",
    //            Direction = ParameterDirection.Output
    //        };

    //        // Execute refund insertion and get RefundId
    //        odal.ExecuteNonQuery("insert_IPAdvRefund_1", CommandType.StoredProcedure, RefundId, refundEntity);

    //        // Retrieve the output RefundId
    //        string refundId = RefundId.Value.ToString();

    //        // Prepare Advance Header entity
    //        var advanceHeaderEntity = objAdvanceHeader.ToDictionary();
    //        RemoveUnnecessaryProperties(advanceHeaderEntity, new[] { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" });

    //        // Execute advance header update
    //        odal.ExecuteNonQuery("update_AdvanceHeader_1", CommandType.StoredProcedure, advanceHeaderEntity);

    //        // Prepare Advance Detail entity
    //        var advanceDetailEntity = objAdvRefundDetail.ToDictionary();
    //        RemoveUnnecessaryProperties(advanceDetailEntity, new[] { "IsCancelled", "IsCancelledby", "IsCancelledDate" });

    //        // Execute advance refund detail insertion
    //        odal.ExecuteNonQuery("insert_AdvRefundDetail_1", CommandType.StoredProcedure, advanceDetailEntity);

    //        // Prepare Advance Detail Balance entity
    //        var balanceEntity = objAdvanceDetail.ToDictionary();
    //        RemoveUnnecessaryProperties(balanceEntity, new[] { "IsCancelled", "IsCancelledby", "IsCancelledDate" });

    //        // Execute advance detail balance amount update
    //        odal.ExecuteNonQuery("update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, balanceEntity);

    //        // Prepare Payment entity
    //        var paymentEntity = objPayment.ToDictionary();
    //        RemoveUnnecessaryProperties(paymentEntity, new[] { "IsCancelled", "IsCancelledBy", "IsCancelledDate" });

    //        // Execute payment insertion
    //        odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure, paymentEntity);

    //        // Update objects with the new RefundId
    //        UpdateIds(objRefund, objAdvanceHeader, objAdvRefundDetail, objAdvanceDetail, objPayment, refundId);
    //    }

    //    public void RemoveUnnecessaryProperties(Dictionary<string, object> entity, string[] propertiesToRemove)
    //    {
    //        foreach (var property in propertiesToRemove)
    //        {
    //            entity.Remove(property);
    //        }
    //    }

    //    private void UpdateIds(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, string refundId)
    //    {
    //        int parsedRefundId = Convert.ToInt32(refundId);

    //        objRefund.RefundId = parsedRefundId;
    //        objAdvanceHeader.AdvanceId = parsedRefundId; // Ensure this is the intended mapping
    //        objAdvRefundDetail.AdvRefId = parsedRefundId;
    //        objAdvanceDetail.AdvanceDetailId = parsedRefundId;
    //        objPayment.PaymentId = parsedRefundId;
    //    }
    //}
}

