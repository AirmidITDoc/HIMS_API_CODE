using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public class BillCancellationService : IBillCancellationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillCancellationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task UpdateAsyncOp(Bill objOpBillCancellation, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Payments", "BillDetails", "AddCharges", "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "IsCancelled", "PbillNo", "TotalAdvanceAmount", "AdvanceUsedAmount", "AddedBy", "CashCounterId", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "IsBillCheck", "SpeTaxPer", "SpeTaxAmt", "IsBillShrHold", "DiscComments", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "CompDiscAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo" };
            var entity = objOpBillCancellation.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            
            odal.ExecuteNonQuery("ps_OP_BILL_CANCELLATION", CommandType.StoredProcedure, entity);           
           
            await _context.SaveChangesAsync(UserId, Username);      
        }
        public virtual async Task UpdateAsyncIp(Bill objIPBillCancellation, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Payments", "BillDetails", "AddCharges", "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "IsCancelled", "PbillNo", "TotalAdvanceAmount", "AdvanceUsedAmount", "AddedBy", "CashCounterId", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "IsBillCheck", "SpeTaxPer", "SpeTaxAmt", "IsBillShrHold", "DiscComments", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "CompDiscAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo" };
            var entity = objIPBillCancellation.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_IP_BILL_CANCELLATION", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }

    }
}
