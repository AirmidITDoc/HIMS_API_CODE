using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System.Transactions;
//using HIMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.DietkitchenMaster
{
    public class FoodItemmasterService : IFoodItemmasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public FoodItemmasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        //public virtual async Task InsertAsync(MFoodItemMaster ObjMFoodItemMaster, int CurrentUserId, string CurrentUserName)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    var lastSeqNoStr = await _context.MFoodItemMasters
        //        .OrderByDescending(x => x.FoodCode)
        //        .Select(x => x.FoodCode)
        //        .FirstOrDefaultAsync();

        //    int lastSeqNo = 0;
        //    if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
        //        lastSeqNo = parsed;

        //    // Increment the sequence number
        //    int newSeqNo = lastSeqNo + 1;
        //    ObjMFoodItemMaster.FoodCode = newSeqNo.ToString();

        //    ObjMFoodItemMaster.CreatedBy = CurrentUserId;
        //    ObjMFoodItemMaster.CreatedDate = AppTime.Now;

        //    _context.MFoodItemMasters.Add(ObjMFoodItemMaster);

        //    await _context.SaveChangesAsync();

        //    scope.Complete();
        //}
    
        public virtual async Task InsertAsync(MFoodItemMaster ObjMFoodItemMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            var lastCode = await _context.MFoodItemMasters.OrderByDescending(x => x.FoodItemId).Select(x => x.FoodCode).FirstOrDefaultAsync();

            int newCode = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                var code = lastCode.Replace("FC", "");

                if (int.TryParse(code, out int lastNumber))
                {
                    newCode = lastNumber + 1;
                }
            }
            ObjMFoodItemMaster.FoodCode = $"FC{newCode:D3}";

            ObjMFoodItemMaster.CreatedBy = UserId;
            ObjMFoodItemMaster.CreatedDate = AppTime.Now;

            _context.MFoodItemMasters.Add(ObjMFoodItemMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(  MFoodItemMaster ObjMFoodItemMaster, int UserId,  string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            // Attach and update Food Item
            _context.Attach(ObjMFoodItemMaster);
            _context.Entry(ObjMFoodItemMaster).State = EntityState.Modified;

            // FoodCode should not be changed during Edit
            _context.Entry(ObjMFoodItemMaster)
                .Property(x => x.FoodCode)
                .IsModified = false;

            // Set modified information
            ObjMFoodItemMaster.ModifiedBy = UserId;
            ObjMFoodItemMaster.ModifiedDate = AppTime.Now;

            // Ignore requested columns
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMFoodItemMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }

}
