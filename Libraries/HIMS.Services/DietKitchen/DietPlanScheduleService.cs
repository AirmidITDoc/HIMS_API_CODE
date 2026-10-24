using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{
    public class DietPlanScheduleService : IDietPlanScheduleService
    {
        private readonly HIMSDbContext _context;

        public DietPlanScheduleService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<DietPlanScheduleListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPlanScheduleListDto>(model, "ps_Rtrv_DietPlanSchedule");
        }

        public virtual async Task<IPagedList<DietPlanDetailListDto>> DietplandetailsListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPlanDetailListDto>(model, "ps_Rtrv_DietPlanDetails");
        }

        public virtual async Task InsertAsync(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            objDietPlanSchedule.CreatedBy = UserId;
            objDietPlanSchedule.CreatedDate = AppTime.Now;
            objDietPlanSchedule.ModifiedBy = UserId;
            objDietPlanSchedule.ModifiedDate = AppTime.Now;
            objDietPlanSchedule.IsCancelled = false;

            if (objDietPlanSchedule.TDietPlanScheduleDetails != null)
            {
                foreach (var detail in objDietPlanSchedule.TDietPlanScheduleDetails)
                {
                    detail.CreatedBy = UserId;
                    detail.CreatedDate = AppTime.Now;
                    detail.ModifiedBy = UserId;
                    detail.ModifiedDate = AppTime.Now;
                    detail.IsCancelled = false;
                }
            }

            _context.TDietPlanSchedules.Add(objDietPlanSchedule);
            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            long dietPlanId = objDietPlanSchedule.DietPlanId;

            var newDetails = objDietPlanSchedule.TDietPlanScheduleDetails?.Cast<TDietPlanScheduleDetail>().ToList() ?? new List<TDietPlanScheduleDetail>();
            objDietPlanSchedule.TDietPlanScheduleDetails = null;

            // Delete existing details first using correct DbSet name
            var lstDetails = await _context.TDietPlanScheduleDetails.Where(x => x.DietPlanId == dietPlanId).ToListAsync();
            if (lstDetails.Any())
                _context.TDietPlanScheduleDetails.RemoveRange(lstDetails);

            await _context.SaveChangesAsync();

            // Attach and update header
            _context.Attach(objDietPlanSchedule);
            _context.Entry(objDietPlanSchedule).State = EntityState.Modified;

            objDietPlanSchedule.ModifiedBy = UserId;
            objDietPlanSchedule.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(objDietPlanSchedule).Property(column).IsModified = false;
            }

            // Re-insert new details with proper foreign key and audit fields
            foreach (var detail in newDetails)
            {
                detail.DietPlanDetId = 0; 
                detail.DietPlanId = dietPlanId;
                detail.CreatedBy = UserId;
                detail.CreatedDate = AppTime.Now;
                detail.ModifiedBy = UserId;
                detail.ModifiedDate = AppTime.Now;
                detail.IsCancelled = false;
            }
            await _context.TDietPlanScheduleDetails.AddRangeAsync(newDetails);

            await _context.SaveChangesAsync();
            scope.Complete();
        }
    }
}