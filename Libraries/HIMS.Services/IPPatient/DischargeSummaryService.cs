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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly HIMSDbContext _context;
        public DischargeSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(DischargeSummary objDischargeSummary, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.DischargeSummaries.Add(objDischargeSummary);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(DischargeSummary objDischargeSummary, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //Update header &detail table records
                _context.DischargeSummaries.Update(objDischargeSummary);
                _context.Entry(objDischargeSummary).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }

        }
    }
}
