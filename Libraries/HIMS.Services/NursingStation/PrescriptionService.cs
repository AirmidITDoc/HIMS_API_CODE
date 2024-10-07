using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using LinqToDB;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.NursingStation
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly HIMSDbContext _context;
        public PrescriptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }



        public virtual async Task InsertAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPrescriptions.Add(objPrescription);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }



        public virtual async Task UpdateAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TPrescriptions.Update(objPrescription);
                _context.Entry(objPrescription).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
