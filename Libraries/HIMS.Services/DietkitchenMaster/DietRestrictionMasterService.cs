using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System.Transactions;
using HIMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.DietkitchenMaster
{
    public class DietRestrictionMasterService : IDietRestrictionMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietRestrictionMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MDietRestrictionMaster ObjMDietRestrictionMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "REST";

            var restrictionCodes = await _context.MDietRestrictionMasters
                .Where(x => !string.IsNullOrEmpty(x.RestrictionCode) && x.RestrictionCode.StartsWith(prefix))
                .Select(x => x.RestrictionCode)
                .ToListAsync();

            int lastSeqNo = restrictionCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;
            ObjMDietRestrictionMaster.RestrictionCode = prefix + newSeqNo.ToString("D3");

            ObjMDietRestrictionMaster.CreatedBy = CurrentUserId;
            ObjMDietRestrictionMaster.CreatedDate = AppTime.Now;
            ObjMDietRestrictionMaster.ModifiedBy = CurrentUserId;
            ObjMDietRestrictionMaster.ModifiedDate = AppTime.Now;

            _context.MDietRestrictionMasters.Add(ObjMDietRestrictionMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(MDietRestrictionMaster ObjMDietRestrictionMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMDietRestrictionMaster);
            _context.Entry(ObjMDietRestrictionMaster).State = EntityState.Modified;

            _context.Entry(ObjMDietRestrictionMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMDietRestrictionMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMDietRestrictionMaster).Property(x => x.RestrictionCode).IsModified = false;

            ObjMDietRestrictionMaster.ModifiedBy = UserId;
            ObjMDietRestrictionMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMDietRestrictionMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}