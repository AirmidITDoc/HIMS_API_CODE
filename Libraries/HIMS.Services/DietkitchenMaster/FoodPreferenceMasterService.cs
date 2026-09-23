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
    public class FoodPreferenceMasterService : IFoodPreferenceMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public FoodPreferenceMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MFoodPreferenceMaster ObjMFoodPreferenceMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "FP";

            var foodPreferenceCodes = await _context.MFoodPreferenceMasters
                .Where(x => !string.IsNullOrEmpty(x.FoodPreferenceCode) && x.FoodPreferenceCode.StartsWith(prefix))
                .Select(x => x.FoodPreferenceCode)
                .ToListAsync();

            int lastSeqNo = foodPreferenceCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            ObjMFoodPreferenceMaster.FoodPreferenceCode = prefix + newSeqNo.ToString("D3");

            ObjMFoodPreferenceMaster.CreatedBy = CurrentUserId;
            ObjMFoodPreferenceMaster.CreatedDate = AppTime.Now;
            ObjMFoodPreferenceMaster.ModifiedBy = CurrentUserId;
            ObjMFoodPreferenceMaster.ModifiedDate = AppTime.Now;

            _context.MFoodPreferenceMasters.Add(ObjMFoodPreferenceMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(MFoodPreferenceMaster ObjMFoodPreferenceMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMFoodPreferenceMaster);
            _context.Entry(ObjMFoodPreferenceMaster).State = EntityState.Modified;

            _context.Entry(ObjMFoodPreferenceMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMFoodPreferenceMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMFoodPreferenceMaster).Property(x => x.FoodPreferenceCode).IsModified = false;

            ObjMFoodPreferenceMaster.ModifiedBy = UserId;
            ObjMFoodPreferenceMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMFoodPreferenceMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}