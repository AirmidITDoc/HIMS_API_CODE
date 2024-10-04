using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.Common
{
    public class PaymentService:IPaymentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PaymentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Payment objPayment, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rPaymentEntity = { "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount", "BillNoNavigation" };

            var entity = objPayment.ToDictionary();
            foreach (var rProperty in rPaymentEntity)
            {
                entity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("v_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);

            //Edmx
            //_context.Payments.Add(objPayment);
            //await _context.SaveChangesAsync();
            
            await _context.SaveChangesAsync(UserId, Username);
        }

        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username)
        {
            // throw new NotImplementedException();
            _context.Payments.Update(objPayment);
            _context.Entry(objPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //scope.Complete();
        }
    }
}
