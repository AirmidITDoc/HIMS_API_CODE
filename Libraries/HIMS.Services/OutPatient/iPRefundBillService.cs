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
    public class ipRefundBillService : IipRefundBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ipRefundBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Refund objIPRefund, TRefundDetail objIPTRefundDetail, int UserId, string Username)

        {
            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate" };
            var entity = objIPRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objIPRefund.RefundId = Convert.ToInt32(RefundId);


            string[] rRefundEntity = { "UpdatedBy", "RefundDetailsTime", "HospitalAmount", "DoctorAmount" };
            var RefundEntity = objIPTRefundDetail.ToDictionary();
            foreach (var rProperty in rRefundEntity)
            {
                RefundEntity.Remove(rProperty);
            }
            string RefundDetId = odal.ExecuteNonQuery("m_insert_T_RefundDetails_1", CommandType.StoredProcedure, "RefundDetId", RefundEntity);
            objIPTRefundDetail.RefundDetId = Convert.ToInt32(RefundDetId);


        }
    }
}
