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
    public  class DietTypeMasterService: IDietTypeMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietTypeMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MDietTypeMaster OBJMDietTypeMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "DT";

            var dietCodes = await _context.MDietTypeMasters
                .Where(x => !string.IsNullOrEmpty(x.DietCode) && x.DietCode.StartsWith(prefix))
                .Select(x => x.DietCode)
                .ToListAsync();

            // Strip prefix, parse numeric part, get max
            int lastSeqNo = dietCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            OBJMDietTypeMaster.DietCode = prefix + newSeqNo.ToString("D2");

            // Get max DisplayOrder and increment
            int lastDisplayOrder = await _context.MDietTypeMasters
                .Select(x => (int?)x.DisplayOrder)
                .MaxAsync() ?? 0;

            OBJMDietTypeMaster.DisplayOrder = lastDisplayOrder + 1;

            OBJMDietTypeMaster.CreatedBy = UserId;
            OBJMDietTypeMaster.CreatedDate = AppTime.Now;

            _context.MDietTypeMasters.Add(OBJMDietTypeMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(MDietTypeMaster OBJMDietTypeMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(OBJMDietTypeMaster);
            _context.Entry(OBJMDietTypeMaster).State = EntityState.Modified;

            _context.Entry(OBJMDietTypeMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(OBJMDietTypeMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(OBJMDietTypeMaster).Property(x => x.DietCode).IsModified = false;
            _context.Entry(OBJMDietTypeMaster).Property(x => x.DisplayOrder).IsModified = false;

            OBJMDietTypeMaster.ModifiedBy = UserId;
            OBJMDietTypeMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(OBJMDietTypeMaster).Property(column).IsModified = false;
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
