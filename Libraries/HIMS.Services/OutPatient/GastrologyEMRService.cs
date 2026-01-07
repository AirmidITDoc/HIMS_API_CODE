using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public  class GastrologyEMRService: IGastrologyEMRService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public GastrologyEMRService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(ClinicalQuesHeader ObjClinicalQuesHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                ObjClinicalQuesHeader.CreatedBy = UserId;
                ObjClinicalQuesHeader.CreatedDate = AppTime.Now;

                _context.ClinicalQuesHeaders.Add(ObjClinicalQuesHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(ClinicalQuesHeader ObjClinicalQuesHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long ClinicalQuesHeaderId = ObjClinicalQuesHeader.ClinicalQuesHeaderId;

                //  Delete related details first
                var lstAttend = await _context.ClinicalQuesDetails
                    .Where(x => x.ClinicalQuesHeaderId == ClinicalQuesHeaderId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.ClinicalQuesDetails.RemoveRange(lstAttend);

             
                //Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjClinicalQuesHeader);
                _context.Entry(ObjClinicalQuesHeader).State = EntityState.Modified;

                _context.Entry(ObjClinicalQuesHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjClinicalQuesHeader).Property(x => x.CreatedDate).IsModified = false;

                ObjClinicalQuesHeader.ModifiedBy = UserId;
                ObjClinicalQuesHeader.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjClinicalQuesHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }


    }
}
