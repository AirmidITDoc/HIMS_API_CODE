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

namespace HIMS.Services.IPPatient
{
    public class IPDraftBillSerive:IIPDraftBillSerive
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPDraftBillSerive(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(TDrbill objBill, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();
            try
            {
                // Bill Code
                DatabaseHelper odal = new();
                string[] rEntity = {"IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "BillNo", "CompDiscAmt", "DiscComments", "CashCounterId"};
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("v_insert_DRBill_1", CommandType.StoredProcedure, "Drbno", entity);
                objBill.Drbno = Convert.ToInt32(vBillNo);

                TDrbillDet objTDrbillDet = new TDrbillDet();
                objTDrbillDet.Drno = Convert.ToInt32(vBillNo);
                _context.TDrbillDets.Add(objTDrbillDet);
                await _context.SaveChangesAsync();

                //foreach (var objItem in objBill.BillDetails)
                //{
                //    objItem.BillNo = objBill.BillNo;
                //    objItem.ChargesId = objItem1?.ChargesId;
                //    _context.TDrbillDets.Add(objItem);
                //    await _context.SaveChangesAsync();
                //}

            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.Drbno);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }
    }
}
