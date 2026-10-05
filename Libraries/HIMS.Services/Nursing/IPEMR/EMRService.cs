using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Nursing.IPEMR;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
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

        public virtual async Task<IPagedList<IPEMRFamilyMedicalHistoryListDto>> GetIPEMRFamilyMedicalHistoryAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPEMRFamilyMedicalHistoryListDto>(model, "ps_GetIP_EMRFamilyMedicalHistory");
        }

        public virtual async Task<IPagedList<IPEMRDignosisHistoryListDto>> GetIPEMRDignosisHistoryAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPEMRDignosisHistoryListDto>(model, "ps_GetIP_EMRDignosisHistory");
        }


        public virtual async Task<IPagedList<IPEMRDiagnosisInfoListDto>> GetIPEMRDiagnosisInfoAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPEMRDiagnosisInfoListDto>(model, "ps_GetIP_EMRDiagnosisInfo");
        }


        public async Task<TIpEmrhistory?> GetByIdAsync(long id)
        {
            return await _context.TIpEmrhistories.Include(x => x.TIpEmrdiagnosisInfos).Include(x => x.TIpEmrdignosisHistories).Include(x => x.TIpEmrVitals).FirstOrDefaultAsync(x => x.IpdEmrId == id);
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
                long ipEmrId = objEMR.IpdEmrId;

                // Delete related details using parent foreign key (Ipemrid) instead of primary key
                var lstAttend = await _context.TIpEmrdiagnosisInfos.Where(x => x.Ipemrid == ipEmrId).ToListAsync();
                if (lstAttend.Any())
                    _context.TIpEmrdiagnosisInfos.RemoveRange(lstAttend);

                var lstSurgery = await _context.TIpEmrdignosisHistories.Where(x => x.Ipemrid == ipEmrId).ToListAsync();
                if (lstSurgery.Any())
                    _context.TIpEmrdignosisHistories.RemoveRange(lstSurgery);

                //var lstDiagnosis = await _context.TIpEmrfamilyMedicalHistories.Where(x => x.IpEmrId == ipEmrId).ToListAsync();
                //if (lstDiagnosis.Any())
                //    _context.TIpEmrfamilyMedicalHistories.RemoveRange(lstDiagnosis);

                var lstVitals = await _context.TIpEmrVitals.Where(x => x.IpemrId == ipEmrId).ToListAsync();
                if (lstVitals.Any())
                    _context.TIpEmrVitals.RemoveRange(lstVitals);

                // Save deletion first
                await _context.SaveChangesAsync(UserId, Username);

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

                await _context.SaveChangesAsync(UserId, Username);
                scope.Complete();
            }
        }

        public virtual async Task InsertFamilyHistoryAsync(TIpEmrfamilyMedicalHistory objFamilyHistory, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpEmrfamilyMedicalHistories.Add(objFamilyHistory);
                await _context.SaveChangesAsync(UserId, Username);

                scope.Complete();
            }
        }

        public virtual async Task UpdateFamilyHistoryAsync(TIpEmrfamilyMedicalHistory objFamilyHistory, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(objFamilyHistory);
            _context.Entry(objFamilyHistory).State = EntityState.Modified;

            _context.Entry(objFamilyHistory).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(objFamilyHistory).Property(x => x.CreatedDate).IsModified = false;

            objFamilyHistory.ModifiedBy = UserId;
            objFamilyHistory.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(objFamilyHistory).Property(column).IsModified = false;
            }

            await _context.SaveChangesAsync();
            scope.Complete();
        }

        public virtual async Task<TIpEmrfamilyMedicalHistory> GetFamilyHistoryByIdAsync(int id)
        {
            return await this._context.TIpEmrfamilyMedicalHistories.FirstOrDefaultAsync(x => x.RegId == id);
        }
        public virtual async Task<IPagedList<FamilyMedicalHistoryListDto>> FamilyMedicalHistoryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<FamilyMedicalHistoryListDto>(model, "ps_rtrv_FamilyMedicalHistory");
        }

    }
}