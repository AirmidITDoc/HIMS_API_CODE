using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using System.Transactions;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.DietKitchen
{
    public class MealtypemasterService : IMealtypemasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MealtypemasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(  MMealTypeMaster ObjMMealTypeMaster,int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            // Generate MealSequence
            var lastMealSequence = await _context.MMealTypeMasters
                .Select(x => (int?)x.MealSequence)
                .MaxAsync() ?? 0;

            int newMealSequence = lastMealSequence + 1;

            // Generate MealTypeCode
            var mealTypeCodes = await _context.MMealTypeMasters
                .Where(x => x.MealTypeCode != null && x.MealTypeCode != "")
                .Select(x => x.MealTypeCode)
                .ToListAsync();

            int lastMealTypeCode = mealTypeCodes
                .Where(x => int.TryParse(x, out _))
                .Select(x => int.Parse(x))
                .DefaultIfEmpty(0)
                .Max();

            int newMealTypeCode = lastMealTypeCode + 1;

            ObjMMealTypeMaster.MealSequence = newMealSequence;
            ObjMMealTypeMaster.MealTypeCode = newMealTypeCode.ToString();

            ObjMMealTypeMaster.CreatedBy = CurrentUserId;
            ObjMMealTypeMaster.CreatedDate = AppTime.Now;

            _context.MMealTypeMasters.Add(ObjMMealTypeMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(MMealTypeMaster ObjMMealTypeMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            // Attach and update Food Item
            _context.Attach(ObjMMealTypeMaster);
            _context.Entry(ObjMMealTypeMaster).State = EntityState.Modified;

            // FoodCode should not be changed during Edit
            _context.Entry(ObjMMealTypeMaster)
                .Property(x => x.MealTypeCode)
                .IsModified = false;

            // FoodCode should not be changed during Edit
            _context.Entry(ObjMMealTypeMaster)
                .Property(x => x.MealSequence)
                .IsModified = false;

            // Set modified information
            ObjMMealTypeMaster.ModifiedBy = UserId;
            ObjMMealTypeMaster.ModifiedDate = AppTime.Now;

            // Ignore requested columns
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMMealTypeMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
