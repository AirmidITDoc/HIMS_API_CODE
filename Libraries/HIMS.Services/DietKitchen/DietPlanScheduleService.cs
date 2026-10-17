using System.Linq;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using System.Threading.Tasks;
using System.Transactions;
using System.Collections.Generic;
using System;

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

        public virtual async Task InsertAsync(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled);

            objDietPlanSchedule.CreatedBy = UserId;
            objDietPlanSchedule.CreatedDate = AppTime.Now;
            objDietPlanSchedule.ModifiedBy = UserId;
            objDietPlanSchedule.ModifiedDate = AppTime.Now;
            objDietPlanSchedule.IsCancelled = false;

            _context.TDietPlanSchedules.Add(objDietPlanSchedule);
            await _context.SaveChangesAsync(UserId, Username);

            scope.Complete();
        }

        
        public virtual async Task InsertAsync(List<TDietPlanSchedule> objDietPlanSchedules, int UserId, string Username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled
            );

            foreach (var item in objDietPlanSchedules)
            {
                item.CreatedBy = UserId;
                item.CreatedDate = AppTime.Now;
                item.ModifiedBy = UserId;
                item.ModifiedDate = AppTime.Now;
                item.IsCancelled = false;

                _context.TDietPlanSchedules.Add(item);
            }

            await _context.SaveChangesAsync(UserId, Username);
            scope.Complete();
        }
    }
}

