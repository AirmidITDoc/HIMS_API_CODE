using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OTManagment
{
    public class OTAnesthesiaService : IOTAnesthesiaService
    {
        private readonly HIMSDbContext _context;
        public OTAnesthesiaService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TOtAnesthesiaRecord ObjTOtAnesthesiaRecord, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtAnesthesiaRecords.Add(ObjTOtAnesthesiaRecord);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task<List<TOtAnesthesiaPreOpdiagnosisDto>> OtAnesthesiaPreOpdiagnosisListAsync(string DescriptionType)
        {
            var query = _context.TOtAnesthesiaPreOpdiagnoses.AsQueryable();

            if (!string.IsNullOrEmpty(DescriptionType))
            {
                string lowered = DescriptionType.ToLower();
                query = query.Where(d => d.DescriptionType != null && d.DescriptionType.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.OtanesthesiaPreOpdiagnosisId)
                .Select(d => new TOtAnesthesiaPreOpdiagnosisDto
                {
                    OtanesthesiaPreOpdiagnosisId = d.OtanesthesiaPreOpdiagnosisId,
                    DescriptionType = d.DescriptionType,
                    DescriptionName = d.DescriptionName
                })
                .Take(50)
                .ToListAsync();

            return data;
        }
        public virtual async Task UpdateAsync(TOtAnesthesiaRecord ObjTOtAnesthesiaRecord, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(ObjTOtAnesthesiaRecord);
                _context.Entry(ObjTOtAnesthesiaRecord).State = EntityState.Modified;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTOtAnesthesiaRecord).Property(column).IsModified = false;
                    }
                }
                // Delete details table realted records
                var lst = await _context.TOtAnesthesiaPreOpdiagnoses.Where(x => x.AnesthesiaId == ObjTOtAnesthesiaRecord.AnesthesiaId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TOtAnesthesiaPreOpdiagnoses.RemoveRange(lst);
                }

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }

}
