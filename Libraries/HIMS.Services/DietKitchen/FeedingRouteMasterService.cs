using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietkitchenMaster
{
    public class FeedingRouteMasterService : IFeedingRouteMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public FeedingRouteMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "FR";

            var feedingRouteCodes = await _context.MFeedingRouteMasters
                .Where(x => !string.IsNullOrEmpty(x.FeedingRouteCode) && x.FeedingRouteCode.StartsWith(prefix))
                .Select(x => x.FeedingRouteCode)
                .ToListAsync();

            int lastSeqNo = feedingRouteCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            ObjMFeedingRouteMaster.FeedingRouteCode = prefix + newSeqNo.ToString("D3");

            ObjMFeedingRouteMaster.CreatedBy = CurrentUserId;
            ObjMFeedingRouteMaster.CreatedDate = AppTime.Now;
            ObjMFeedingRouteMaster.ModifiedBy = CurrentUserId;
            ObjMFeedingRouteMaster.ModifiedDate = AppTime.Now;

            _context.MFeedingRouteMasters.Add(ObjMFeedingRouteMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMFeedingRouteMaster);
            _context.Entry(ObjMFeedingRouteMaster).State = EntityState.Modified;

            _context.Entry(ObjMFeedingRouteMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMFeedingRouteMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMFeedingRouteMaster).Property(x => x.FeedingRouteCode).IsModified = false;

            ObjMFeedingRouteMaster.ModifiedBy = UserId;
            ObjMFeedingRouteMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMFeedingRouteMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}