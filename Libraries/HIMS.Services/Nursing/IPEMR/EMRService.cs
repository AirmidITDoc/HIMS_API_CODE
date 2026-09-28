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
            return await _context.TIpEmrhistories
                .Include(x => x.TIpEmrdiagnosisInfos)
                .Include(x => x.TIpEmrdignosisHistories)
                .Include(x => x.TIpEmrfamilyMedicalHistory)
                .FirstOrDefaultAsync(x => x.IpdEmrId == id);
        }

        public virtual async Task InsertAsync(TIpEmrhistory objEMR, int userId, string username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            objEMR.CreatedBy = userId;
            objEMR.CreatedDate = AppTime.Now;

            foreach (var item in objEMR.TIpEmrdiagnosisInfos)
            {
                item.CreatedBy = userId;
                item.CreatedDate = AppTime.Now;
            }

            foreach (var item in objEMR.TIpEmrdignosisHistories)
            {
                item.CreatedBy = userId;
                item.CreatedDate = AppTime.Now;
            }

            if (objEMR.TIpEmrfamilyMedicalHistory != null)
            {
                objEMR.TIpEmrfamilyMedicalHistory.Fhist = objEMR;
                
                objEMR.TIpEmrfamilyMedicalHistory.CreatedBy = userId;
                objEMR.TIpEmrfamilyMedicalHistory.CreatedDate = AppTime.Now;
            }

            _context.TIpEmrhistories.Add(objEMR);
            await _context.SaveChangesAsync(); 

            scope.Complete();
        }

        public virtual async Task UpdateAsync(TIpEmrhistory objEMR, int userId, string username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            long emrId = objEMR.IpdEmrId;

            var lstDiagnosisInfo = await _context.TIpEmrdiagnosisInfos.Where(x => x.Ipemrid == emrId).ToListAsync();
            if (lstDiagnosisInfo.Any()) _context.TIpEmrdiagnosisInfos.RemoveRange(lstDiagnosisInfo);

            var lstDiagnosisHistory = await _context.TIpEmrdignosisHistories.Where(x => x.Ipemrid == emrId).ToListAsync();
            if (lstDiagnosisHistory.Any()) _context.TIpEmrdignosisHistories.RemoveRange(lstDiagnosisHistory);

            var famHistory = await _context.TIpEmrfamilyMedicalHistories.Where(x => x.IpEmrId == emrId).ToListAsync();
            if (famHistory.Any()) _context.TIpEmrfamilyMedicalHistories.RemoveRange(famHistory);

            await _context.SaveChangesAsync();

            _context.Attach(objEMR);
            _context.Entry(objEMR).State = EntityState.Modified;

            _context.Entry(objEMR).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(objEMR).Property(x => x.CreatedDate).IsModified = false;

            objEMR.ModifiedBy = userId;
            objEMR.ModifiedDate = AppTime.Now;

            foreach (var item in objEMR.TIpEmrdiagnosisInfos)
            {
                item.CreatedBy = userId;
                item.CreatedDate = AppTime.Now;
                item.ModifiedBy = userId;
                item.ModifiedDate = AppTime.Now;
            }

            foreach (var item in objEMR.TIpEmrdignosisHistories)
            {
                item.CreatedBy = userId;
                item.CreatedDate = AppTime.Now;
                item.ModifiedBy = userId;
                item.ModifiedDate = AppTime.Now;
            }

            if (objEMR.TIpEmrfamilyMedicalHistory != null)
            {
                objEMR.TIpEmrfamilyMedicalHistory.Fhist = objEMR;
                objEMR.TIpEmrfamilyMedicalHistory.CreatedBy = userId;
                objEMR.TIpEmrfamilyMedicalHistory.CreatedDate = AppTime.Now;
                objEMR.TIpEmrfamilyMedicalHistory.ModifiedBy = userId;
                objEMR.TIpEmrfamilyMedicalHistory.ModifiedDate = AppTime.Now;
            }

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(objEMR).Property(column).IsModified = false;
            }

            await _context.SaveChangesAsync(); 
            scope.Complete();
        }
    }
}