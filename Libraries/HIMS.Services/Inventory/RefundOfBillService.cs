using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public class RefundOfBillService : IRefundOfBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RefundOfBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Refund objRefund, TRefundDetail objTRefundDetail, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy ", "CreatedDate", "ModifiedDate", " IsActive" };
            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(RegId);
            objTRefundDetail.RefundId = Convert.ToInt32(RegId);

            string[] rRefundEntity = { " RefundDetId", "ChargesId ", " HospitalAmount", "DoctorAmount " };
            var RefundEntity = objTRefundDetail.ToDictionary();
            foreach (var rProperty in rRefundEntity)
            {
                RefundEntity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("m_insert_T_RefundDetails_1", CommandType.StoredProcedure, "RefundDetId", RefundEntity);
            objTRefundDetail.RefundDetId = Convert.ToInt32(VisitId);

            
        }

    }
}



    

