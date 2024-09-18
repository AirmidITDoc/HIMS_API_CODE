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
        public virtual async Task InsertAsyncSP(Refund objRefund, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "CashCounterId", "IsRefundFlag", "TRefundDetails" };
                var entity = objRefund.ToDictionary(); 
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string VRefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
                objRefund.RefundId = Convert.ToInt32(VRefundId);



                //// Add details table records
                //foreach (var objRefundDetails in objRefund.TRefundDetails)
                //{
                //    objRefundDetails.RefundId = objRefund.RefundId;
                //}
                //_context.TRefundDetails.AddRange(objRefund.TRefundDetail);
                //await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                Refund? objSup = await _context.Refunds.FindAsync(objRefund.RefundId);
                if (objSup != null)
                {
                    _context.Refunds.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.TRefundDetails.Where(x => x.RefundId == objRefund.RefundId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TRefundDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }

    }
}



    

