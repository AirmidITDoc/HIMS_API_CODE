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

namespace HIMS.Services.OutPatient
{
    public class AdvanceService : IAdvanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objpayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string AdvanceId = odal.ExecuteNonQuery("insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(AdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(AdvanceId);
            objpayment.AdvanceId = Convert.ToInt32(AdvanceId);
           
            string[] rDetailEntity = { "IsCancelled", "IsCancelledby", "IsCancelledDate" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceEntity);
           

            string[] rPaymentEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate" };

            var PaymentEntity = objpayment.ToDictionary();
            foreach (var rProperty in rPaymentEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure,  PaymentEntity);
           
        }


        public virtual async Task InsertAsyncSP(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] rRefundEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var refundentity = objRefund.ToDictionary();
            foreach (var rProperty in rRefundEntity)
            {
                refundentity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("insert_IPAdvRefund_1", CommandType.StoredProcedure, "RefundId", refundentity);



            string[] rEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
              odal.ExecuteNonQuery("update_AdvanceHeader_1", CommandType.StoredProcedure,  entity);
           



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
            odal.ExecuteNonQuery("update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, BalEntity);




            string[] rPaymentEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate" };

            var PaymentEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPaymentEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure, PaymentEntity);

            objRefund.RefundId = Convert.ToInt32(RefundId);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(RefundId);
            objAdvRefundDetail.AdvRefId = Convert.ToInt32(RefundId);
            objAdvanceDetail.AdvanceDetailId = Convert.ToInt32(RefundId);
            objPayment.PaymentId = Convert.ToInt32(RefundId);

        }
    }
}
