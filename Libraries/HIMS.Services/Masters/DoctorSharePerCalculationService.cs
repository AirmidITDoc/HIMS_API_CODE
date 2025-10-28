using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Masters
{
    public class DoctorSharePerCalculationService : IDoctorSharePerCalculationService
    {
        private readonly HIMSDbContext _context;
        public DoctorSharePerCalculationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual void UpdateOP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {



            DatabaseHelper odal = new();
            string[] rEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "OpdIpdType", "IsCancelled", "PbillNo", "TotalAdvanceAmount", "AdvanceUsedAmount", "AddedBy", "CashCounterId", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "IsBillCheck", "SpeTaxPer", "SpeTaxAmt", "IsBillShrHold", "DiscComments", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "CompDiscAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
            var entity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("OP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, "BillNo", entity);
            //objBill.BillNo = Convert.ToInt32(vBillNo);

        }
        public virtual void UpdateIP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {



            DatabaseHelper odal = new();
            string[] rEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "OpdIpdType", "IsCancelled", "PbillNo", "TotalAdvanceAmount", "AdvanceUsedAmount", "AddedBy", "CashCounterId", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "IsBillCheck", "SpeTaxPer", "SpeTaxAmt", "IsBillShrHold", "DiscComments", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "CompDiscAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
            var entity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("IP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, "BillNo", entity);
            //objBill.BillNo = Convert.ToInt32(vBillNo);

        }
    }
}
