using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using LinqToDB;

namespace HIMS.Services.OutPatient
{
    public class PrescriptionService1 : IPrescriptionService1
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrescriptionService1(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TPrescription objPrescription, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //// Update store table records
                //TPrescription PrescriptionInfo = await _context.TPrescriptions.FirstOrDefaultAsync(x => x.PrecriptionId == objPrescription.PrecriptionId);
                //if (PrescriptionInfo != null)
                //{
                //    PrescriptionInfo.OpdIpdType = Convert.ToString(Convert.ToInt32(PrescriptionInfo?.OpdIpdType ?? "0") + 1);
                //    _context.TPrescriptions.Update(PrescriptionInfo);
                //    await _context.SaveChangesAsync();
                //}

                //// Add header & detail table records
                //objPrescription.OpdIpdType = (PrescriptionInfo != null) ? PrescriptionInfo.OpdIpdType : "0";
                //_context.TPrescriptions.Add(objPrescription);
                //await _context.SaveChangesAsync();

                //scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TPrescription objPrescription, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //// Delete details table realted records
                //var lst = await _context.TPrescriptions.Where(x => x.PrecriptionId == objPrescription.PrecriptionId).ToListAsync();
                //_context.TPrescriptions.RemoveRange(lst);

                // Update header & detail table records
                _context.TPrescriptions.Update(objPrescription);
                _context.Entry(objPrescription).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


    }
}
