using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    
         public class OPPayment : IOPPayment
    {
   
        private readonly Data.Models.HIMSDbContext _context;
        public OPPayment(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual void InsertSP(Payment objPayment, int CurrentUserId, string CurrentUserName)
        {


            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "ReceiptNo", "CashCounterId", "IsSelfOrcompany", "CompanyId", "Neftno", "Ch_CashPayAmount", "Ch_ChequePayAmount", "Ch_CardPayAmount", "Ch_AdvanceUsedAmount", "Ch_NEFTPayAmount", "Ch_PayTMAmount", "TranMode" };

            var entity = objPayment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("m_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);

        }
    }
}
