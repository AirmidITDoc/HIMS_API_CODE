using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class LabRequestService : ILabRequestService
    {

        private readonly Data.Models.HIMSDbContext _context;
        public LabRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      
        public virtual async Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.THlabRequests.Add(objTHlabRequest);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(THlabRequest objTHlabRequest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.THlabRequests.Update(objTHlabRequest);
                _context.Entry(objTHlabRequest).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }

}
