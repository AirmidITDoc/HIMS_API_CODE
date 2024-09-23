using Aspose.Cells.Drawing;
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
    public class OPSettlementService :IOPSettlementService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPSettlementService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();

            string[] rVisitEntity = { "ReceiptNo", "CashCounterId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount", "BillNoNavigation" };
            var payentity = objpayment.ToDictionary(); 
            foreach (var rProperty in rVisitEntity)
            {
                payentity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("m_insert_Payment_OPIP_1", CommandType.StoredProcedure, "PaymentId", payentity);
            objpayment.PaymentId = Convert.ToInt32(PaymentId);
        }

    }
}
