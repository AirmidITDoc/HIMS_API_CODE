using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public class OPRefundOfBillService : IOPRefundOfBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPRefundOfBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Refund objRefund, TRefundDetail objTRefundDetail, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };
 
            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(RefundId);
            objTRefundDetail.RefundId = Convert.ToInt32(RefundId);

            string[] rRefundEntity = { "UpdatedBy", "RefundDetailsTime", "HospitalAmount", "DoctorAmount" , "RefundDetId"};
            var RefundEntity = objTRefundDetail.ToDictionary();
            foreach (var rProperty in rRefundEntity)
            {
                RefundEntity.Remove(rProperty);
            }
             odal.ExecuteNonQuery("m_insert_T_RefundDetails_1", CommandType.StoredProcedure,  RefundEntity);


        }
        public virtual async Task InsertAsync(Refund objRefund, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate" };
            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(RefundId);

            //// Add details table records
            foreach (var objRefundDet in objRefund.TRefundDetails)
            {
                objRefundDet.RefundId = objRefund.RefundId;
            }
            _context.TRefundDetails.AddRange(objRefund.TRefundDetails);
            await _context.SaveChangesAsync();

        }

    }
}
