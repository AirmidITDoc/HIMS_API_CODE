using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.GRN;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{

    public class DietMenuMasterService : IDietMenuMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietMenuMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DietmenumasterListDto>> GetDietmenumasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietmenumasterListDto>(model, "getMDietMenuMasterList");
        }
        public virtual async Task<IPagedList<DietmenuDetailmasterListDto>> GetDietmenumasterDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietmenuDetailmasterListDto>(model, "getMDietMenuDetailsMasterList");
        }
        public virtual async Task InsertAsync( MDietMenuMaster ObjMDietMenuMaster, int UserId,string Username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            var lastCode = await _context.MDietMenuMasters.OrderByDescending(x => x.DietMenuId) .Select(x => x.DietMenuCode) .FirstOrDefaultAsync();

            int newCode = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                var code = lastCode.Replace("DM", "");

                if (int.TryParse(code, out int lastNumber))
                {
                    newCode = lastNumber + 1;
                }
            }
            ObjMDietMenuMaster.DietMenuCode = $"DM{newCode:D3}";

            ObjMDietMenuMaster.CreatedBy = UserId;
            ObjMDietMenuMaster.CreatedDate = AppTime.Now;

            _context.MDietMenuMasters.Add(ObjMDietMenuMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }



        //public virtual async Task UpdateAsync(MDietMenuMaster ObjMDietMenuMaster, int currentUserId, string currentUserName, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    long dietMenuId = ObjMDietMenuMaster.DietMenuId;

        //    var newDetails = ObjMDietMenuMaster.MDietMenuDetailMasters.ToList() ?? new List<MDietMenuDetailMaster>();
        //    ObjMDietMenuMaster.MDietMenuDetailMasters = null;

        //    // Delete existing related details first
        //    var lstDetails = await _context.MDietMenuDetailMasters
        //        .Where(x => x.DietMenuId == dietMenuId)
        //        .ToListAsync();

        //    if (lstDetails.Any())
        //        _context.MDietMenuDetailMasters.RemoveRange(lstDetails);

        //    // Save deletion first
        //    await _context.SaveChangesAsync();

        //    // Then attach and update header
        //    _context.Attach(ObjMDietMenuMaster);
        //    _context.Entry(ObjMDietMenuMaster).State = EntityState.Modified;

        //    // Prevent DietMenuCode from being modified during update
        //    _context.Entry(ObjMDietMenuMaster).Property(x => x.DietMenuCode).IsModified = false;

        //    ObjMDietMenuMaster.ModifiedBy = currentUserId;
        //    ObjMDietMenuMaster.ModifiedDate = AppTime.Now;

        //    //if (ignoreColumns?.Length > 0)
        //    //{
        //    //    foreach (var column in ignoreColumns)
        //    //        _context.Entry(ObjMDietMenuMaster).Property(column).IsModified = false;
        //    //}

        //    // Re-insert the (new) detail rows against this header
        //    foreach (var detail in newDetails)
        //    {
        //        detail.DietMenuId = dietMenuId;
        //        detail.CreatedBy = currentUserId;
        //        detail.CreatedDate = AppTime.Now;
        //    }
        //    await _context.MDietMenuDetailMasters.AddRangeAsync(newDetails);
        //    // Re-insert the (new) detail rows against this header

        //    await _context.SaveChangesAsync();
        //    scope.Complete();
        //}

        public virtual async Task UpdateAsync(
    MDietMenuMaster ObjMDietMenuMaster,
    int UserId,
    string Username,
    string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            long dietMenuId = ObjMDietMenuMaster.DietMenuId;

            var newDetails = ObjMDietMenuMaster.MDietMenuDetailMasters?.ToList()
                             ?? new List<MDietMenuDetailMaster>();

            ObjMDietMenuMaster.MDietMenuDetailMasters = null;

            // Delete existing related details
            var lstDetails = await _context.MDietMenuDetailMasters
                .Where(x => x.DietMenuId == dietMenuId)
                .ToListAsync();

            if (lstDetails.Any())
            {
                _context.MDietMenuDetailMasters.RemoveRange(lstDetails);
            }

            // Save deletion
            await _context.SaveChangesAsync();

            // Update header
            _context.Attach(ObjMDietMenuMaster);
            _context.Entry(ObjMDietMenuMaster).State = EntityState.Modified;

            ObjMDietMenuMaster.ModifiedBy = UserId;
            ObjMDietMenuMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMDietMenuMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            // Re-insert new detail rows
            foreach (var detail in newDetails)
            {
                detail.MenuDetId = 0;
                detail.DietMenuId = dietMenuId;
                detail.CreatedBy = UserId;
                detail.CreatedDate = AppTime.Now;
            }

            if (newDetails.Any())
            {
                await _context.MDietMenuDetailMasters.AddRangeAsync(newDetails);
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
    }
