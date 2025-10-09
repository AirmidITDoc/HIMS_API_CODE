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

namespace HIMS.Services.Administration
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly HIMSDbContext _context;
        public PaymentModeService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username)
        {
            // throw new NotImplementedException();
            _context.Payments.Update(objPayment);
            _context.Entry(objPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //scope.Complete();
        }
       
        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
             // Get existing entity from DB
            var existingPayment = await _context.Payments.FindAsync(objPayment.PaymentId);
            if (existingPayment == null)
                throw new Exception("Payment record not found.");

            // Copy updated values (except null ones)
            _context.Entry(existingPayment).CurrentValues.SetValues(objPayment);

            if (objPayment.UnitId == null)
                _context.Entry(existingPayment).Property(p => p.UnitId).IsModified = false;

            // Handle ignore columns (if passed)
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(existingPayment).Property(column).IsModified = false;
                }
            }

            await _context.SaveChangesAsync();
            scope.Complete();
        }


    }
}


