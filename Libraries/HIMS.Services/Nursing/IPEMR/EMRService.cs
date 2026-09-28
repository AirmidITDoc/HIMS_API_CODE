using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Nursing.IPEMR
{
    public class EMRService : IEMRService
    {
        private readonly HIMSDbContext _context;

        public EMRService(HIMSDbContext context)
        {
            _context = context;
        }

        public async Task<TIpEmrhistory?> GetByIdAsync(long id)
        {
            return await _context.TIpEmrhistories.Include(x => x.TIpEmrdiagnosisInfos).Include(x => x.TIpEmrdignosisHistories).Include(x => x.TIpEmrfamilyMedicalHistories).FirstOrDefaultAsync(x => x.IpdEmrId == id);
        }


        public virtual async Task InsertAsync(TIpEmrhistory objEMR, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                objEMR.CreatedBy = UserId;
                objEMR.CreatedDate = AppTime.Now;

                _context.TIpEmrhistories.Add(objEMR);
                await _context.SaveChangesAsync(UserId, Username);       // fixed


                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TIpEmrhistory objEMR, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long IpdEmrId = objEMR.IpdEmrId;

                // Delete related details first
                var lstAttend = await _context.TIpEmrdiagnosisInfos.Where(x => x.IpemrdiagnId == IpdEmrId).ToListAsync();
                if (lstAttend.Any())
                    _context.TIpEmrdiagnosisInfos.RemoveRange(lstAttend);


                var lstSurgery = await _context.TIpEmrdignosisHistories.Where(x => x.EmrdignId == IpdEmrId).ToListAsync();
                if (lstSurgery.Any())
                    _context.TIpEmrdignosisHistories.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TIpEmrfamilyMedicalHistories.Where(x => x.FhistId == IpdEmrId).ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TIpEmrfamilyMedicalHistories.RemoveRange(lstDiagnosis);

                //Save deletion first
                await _context.SaveChangesAsync(UserId, Username);       // fixed

                // Then attach and update header
                _context.Attach(objEMR);
                _context.Entry(objEMR).State = EntityState.Modified;

                _context.Entry(objEMR).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(objEMR).Property(x => x.CreatedDate).IsModified = false;

                objEMR.ModifiedBy = UserId;
                objEMR.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(objEMR).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync(UserId, Username);       // fixed

                scope.Complete();
            }
        }
    }
}