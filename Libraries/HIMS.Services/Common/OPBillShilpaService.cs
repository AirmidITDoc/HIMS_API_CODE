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
using System.Transactions;

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
            try
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
                // Add details table records
                foreach (var objItem in objBill.BillDetails)
                {
                    objItem.BillNo = objBill.BillNo;
                }
                _context.BillDetails.AddRange(objBill.BillDetails);
                await _context.SaveChangesAsync();


            }
            catch (Exception)
            {
                // Delete header table realted records
                Bill? objSup = await _context.Bills.FindAsync(objBill.BillNo);
                if (objSup != null)
                {
                    _context.Bills.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.BillDetails.Where(x => x.BillNo == objBill.BillNo).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.BillDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}

