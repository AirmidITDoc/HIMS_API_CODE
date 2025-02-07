using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MedicalRecordService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpmedicalRecords.Add(objTIpmedicalRecord);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TIpmedicalRecords.Update(objTIpmedicalRecord);
                _context.Entry(objTIpmedicalRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
       
    }
}
