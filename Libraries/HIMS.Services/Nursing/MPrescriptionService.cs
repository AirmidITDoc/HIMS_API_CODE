using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public  class MPrescriptionService : IMPrescriptionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MPrescriptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpmedicalRecords.Add(objmedicalRecord);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        
    }
}
