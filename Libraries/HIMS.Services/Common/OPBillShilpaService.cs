using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Common
{
    public class OPBillShilpaService : IOPBillShilpaService
    {
        private readonly HIMSDbContext _context;
        public OPBillShilpaService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Bill objBill, int UserId, string Username)
        {


            //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails" };
            var entity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("v_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
            objBill.BillNo = Convert.ToInt32(vBillNo);

            // Add details table records
            foreach (var objItem in objBill.BillDetails)
            {
                objItem.BillNo = objBill.BillNo;
            }
            _context.BillDetails.AddRange(objBill.BillDetails);
            await _context.SaveChangesAsync();


        }
        public virtual void InsertSP1(Bill objBill, int UserId, string Username)
        {


            //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
            var entity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("v_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
            objBill.BillNo = Convert.ToInt32(vBillNo);

            string[] DEntity = { "BillDetailId" };
            var Dentity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_BillDetails_1", CommandType.StoredProcedure, Dentity);
        }
    }
}

