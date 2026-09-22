using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.DietkitchenMaster
{
    public class MFoodCategoryMasterService : IMFoodCategoryMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MFoodCategoryMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task InsertAsync( MFoodCategoryMaster ObjMFoodCategoryMaster, int CurrentUserId,string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            var foodCodes = await _context.MFoodCategoryMasters
                .Where(x => x.FoodCategoryCode != null && x.FoodCategoryCode != "")
                .Select(x => x.FoodCategoryCode)
                .ToListAsync();

            int lastSeqNo = foodCodes
                .Where(x => int.TryParse(x, out _))
                .Select(x => int.Parse(x))
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            ObjMFoodCategoryMaster.FoodCategoryCode = newSeqNo.ToString();

            ObjMFoodCategoryMaster.CreatedBy = CurrentUserId;
            ObjMFoodCategoryMaster.CreatedDate = AppTime.Now;
            ObjMFoodCategoryMaster.ModifiedBy = CurrentUserId;
            ObjMFoodCategoryMaster.ModifiedDate = AppTime.Now;
            _context.MFoodCategoryMasters.Add(ObjMFoodCategoryMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }



        public virtual async Task UpdateAsync(MFoodCategoryMaster ObjMFoodCategoryMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            // Attach and update Food Item
            _context.Attach(ObjMFoodCategoryMaster);
            _context.Entry(ObjMFoodCategoryMaster).State = EntityState.Modified;

            // FoodCode should not be changed during Edit
            _context.Entry(ObjMFoodCategoryMaster)
                .Property(x => x.FoodCategoryCode)
                .IsModified = false;

            // Set modified information
            ObjMFoodCategoryMaster.ModifiedBy = UserId;
            ObjMFoodCategoryMaster.ModifiedDate = AppTime.Now;

            // Ignore requested columns
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMFoodCategoryMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}

