using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.DTO.OPPatient;


namespace HIMS.Services
{
    public class OTPreOperationService: IOTPreOperationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTPreOperationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<perOperationsurgeryListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<perOperationsurgeryListDto>(model, "rtrv_perOperationsurgeryList");
        }
        public virtual async Task<IPagedList<PreOperationAttendentListDto>> preOperationAttendentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PreOperationAttendentListDto>(model, "rtrv_preOperationAttendentList");
        }
        public virtual async Task<List<OtpreOperationDiagnosisListDto>> PreOperationDiagnosisListAsync(string DescriptionType)
        {
            var query = _context.TOtPreOperationDiagnoses.AsQueryable();

            if (!string.IsNullOrEmpty(DescriptionType))
            {
                string lowered = DescriptionType.ToLower();
                query = query.Where(d => d.DescriptionType != null && d.DescriptionType.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.OtpreOperationDiagnosisDetId)
                .Select(d => new OtpreOperationDiagnosisListDto
                {
                    OtpreOperationDiagnosisDetId = d.OtpreOperationDiagnosisDetId,
                    DescriptionType = d.DescriptionType,
                    DescriptionName = d.DescriptionName
                })
                .Take(50)
                .ToListAsync();

            return data;
        }
        public virtual async Task<List<OtPreOperationCathlabDiagnosisListDto>> PreOperationCathlabDiagnosisListAsync(string DescriptionType)
        {
            var query = _context.TOtPreOperationCathlabDiagnoses.AsQueryable();

            if (!string.IsNullOrEmpty(DescriptionType))
            {
                string lowered = DescriptionType.ToLower();
                query = query.Where(d => d.DescriptionType != null && d.DescriptionType.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.OtpreOperationCathLabDiagnosisDetId)
                .Select(d => new OtPreOperationCathlabDiagnosisListDto
                {
                    OtpreOperationCathLabDiagnosisDetId = d.OtpreOperationCathLabDiagnosisDetId,
                    DescriptionType = d.DescriptionType,
                    DescriptionName = d.DescriptionName
                })
                .Take(50)
                .ToListAsync();

            return data;
        }

        public virtual async Task InsertAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtPreOperationHeaders
                    .OrderByDescending(x => x.OtpreOperationNo)
                    .Select(x => x.OtpreOperationNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Assign new sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtPreOperationHeader.OtpreOperationNo = newSeqNo.ToString();

                // Audit fields
                ObjTOtPreOperationHeader.Createdby = UserId;
                ObjTOtPreOperationHeader.CreatedDate = DateTime.Now;

                // Insert
                _context.TOtPreOperationHeaders.Add(ObjTOtPreOperationHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long OtpreOperationId = ObjTOtPreOperationHeader.OtpreOperationId;

                // ✅ Delete related details first
                var lstAttend = await _context.TOtPreOperationAttendingDetails
                    .Where(x => x.OtpreOperationId == OtpreOperationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TOtPreOperationAttendingDetails.RemoveRange(lstAttend);

                var lstSurgery = await _context.TOtPreOperationCathlabDiagnoses
                    .Where(x => x.OtpreOperationId == OtpreOperationId)
                    .ToListAsync();
                if (lstSurgery.Any())
                    _context.TOtPreOperationCathlabDiagnoses.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TOtPreOperationDiagnoses
                    .Where(x => x.OtpreOperationId == OtpreOperationId)
                    .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TOtPreOperationDiagnoses.RemoveRange(lstDiagnosis);

                // ✅ Save deletion first
                await _context.SaveChangesAsync();

                // ✅ Then attach and update header
                _context.Attach(ObjTOtPreOperationHeader);
                _context.Entry(ObjTOtPreOperationHeader).State = EntityState.Modified;

                _context.Entry(ObjTOtPreOperationHeader).Property(x => x.Createdby).IsModified = false;
                _context.Entry(ObjTOtPreOperationHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTOtPreOperationHeader).Property(x => x.OtpreOperationNo).IsModified = false;

                ObjTOtPreOperationHeader.ModifiedBy = UserId;
                ObjTOtPreOperationHeader.ModifiedDate = DateTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTOtPreOperationHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }



    }
}
