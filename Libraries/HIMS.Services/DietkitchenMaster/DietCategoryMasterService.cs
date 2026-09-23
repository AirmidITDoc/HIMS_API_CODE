using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietkitchenMaster
{
    public  class DietCategoryMasterService :IDietCategoryMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietCategoryMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(MDietCategoryMaster ObjMDietCategoryMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "DC";

            var dietCodes = await _context.MDietCategoryMasters
                .Where(x => !string.IsNullOrEmpty(x.CategoryCode) && x.CategoryCode.StartsWith(prefix))
                .Select(x => x.CategoryCode)
                .ToListAsync();

            int lastSeqNo = dietCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            ObjMDietCategoryMaster.CategoryCode = prefix + newSeqNo.ToString("D2");

            ObjMDietCategoryMaster.CreatedBy = UserId;
            ObjMDietCategoryMaster.CreatedDate = AppTime.Now;

            _context.MDietCategoryMasters.Add(ObjMDietCategoryMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(MDietCategoryMaster ObjMDietCategoryMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMDietCategoryMaster);
            _context.Entry(ObjMDietCategoryMaster).State = EntityState.Modified;

            _context.Entry(ObjMDietCategoryMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMDietCategoryMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMDietCategoryMaster).Property(x => x.CategoryCode).IsModified = false;

            ObjMDietCategoryMaster.ModifiedBy = UserId;
            ObjMDietCategoryMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(ObjMDietCategoryMaster).Property(column).IsModified = false;
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
