﻿using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class PriscriptionReturnService : IPriscriptionReturnService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PriscriptionReturnService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
       }
        public virtual async Task UpdateAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TIpprescriptionReturnHs.Update(objIpprescriptionReturnH);
                _context.Entry(objIpprescriptionReturnH).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
    
