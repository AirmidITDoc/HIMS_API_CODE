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



namespace HIMS.Services.OutPatient
{
    public class PaymentService : IPaymentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PaymentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(Payment objPayment, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AddBy", "IsCancelledBy", "IsCancelledDate", "IsCancelled"};
            var entity = objPayment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("m_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);

            await _context.SaveChangesAsync(UserId, Username);
        }
        public virtual async Task UpdateAsync(Payment objPayment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.Payments.Update(objPayment);
                _context.Entry(objPayment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
