using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public  class PrescriptionServise: IPrescriptionService
    {
        private readonly HIMSDbContext _context;
        public PrescriptionServise(HIMSDbContext HIMSDbContext)
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
        public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task PrescCancelAsync(TIpPrescription objmedicalRecord, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TIpPrescription ObjLab = await _context.TIpPrescriptions.FindAsync(objmedicalRecord.IppreId);
                if (ObjLab == null)
                    throw new Exception("Prescription not found.");
                // Cancel fields
                ObjLab.IsClosed = true;
              
                _context.TIpPrescriptions.Update(ObjLab);
                _context.Entry(ObjLab).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task PrescreturnCancelAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
              
               
                TIpprescriptionReturnH Obj1= await _context.TIpprescriptionReturnHs.FindAsync(objIpprescriptionReturnH.PresReId);
                if (Obj1 == null)
                    throw new Exception("Prescription Return not found.");
                // Cancel fields
                Obj1.Isclosed = true;

                _context.TIpprescriptionReturnHs.Update(Obj1);
                _context.Entry(Obj1).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();

            }
        }
    }
}
