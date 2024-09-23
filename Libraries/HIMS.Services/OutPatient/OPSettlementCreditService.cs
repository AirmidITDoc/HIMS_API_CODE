using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;
using System.ComponentModel.Design;
using WkHtmlToPdfDotNet;

namespace HIMS.Services.OutPatient
{
    public class OPSettlementCreditService: IOPSettlementCreditService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPSettlementCreditService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Bill objBill, Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            // throw new NotImplementedException();

            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = {"OpdIpdId","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","DateTimeBillDate","byteOpdIpdType","IsCancelled","stringPbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","DateTimeBillTime","ConcessionReasonId",
                "boolIsSettled","boolIsPrinted","boolIsFree","CompanyId","TariffId","UnitId","InterimOrFinal","stringCompanyRefNo","ConcessionAuthorizationName",
                "boolIsBillCheck","SpeTaxPer","SpeTaxAmt","boolIsBillShrHold","stringDiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","stringBillPrefix","stringBillMonth","stringBillYear","stringPrintBillNo"};
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_BillBalAmount_1", CommandType.StoredProcedure, entity);

                // Payment Code

                objpayment.BillNo = objBill.BillNo;
                _context.Payments.Add(objpayment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }
    }
}
