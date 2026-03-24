using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.FeedBack
{
    public  class PatientFeedBackService : IPatientFeedBackService
    {
        private readonly HIMSDbContext _context;
        public PatientFeedBackService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TPatientFeedback ObjTPatientFeedback, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPatientFeedbacks.Add(ObjTPatientFeedback);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync( TPatientFeedback ObjTPatientFeedback, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(  TransactionScopeOption.Required,  new TransactionOptions  {IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted  },   TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjTPatientFeedback);
            var entry = _context.Entry(ObjTPatientFeedback);
            entry.State = EntityState.Modified;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    entry.Property(column).IsModified = false;
                }
            }
            ObjTPatientFeedback.ModifiedBy = UserId;
            ObjTPatientFeedback.ModifiedDate = AppTime.Now;

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
