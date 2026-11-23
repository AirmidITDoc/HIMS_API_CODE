using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Extensions;
using HIMS.Services.Utilities;

namespace HIMS.Services.DietKitchen
{
    public class DietKitchenRequestService : IDietKitchenRequestService
    {
        private readonly HIMSDbContext _context;

        public DietKitchenRequestService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task UpdateDietPlanScheduleStatus(List<TDietPlanScheduleDetail> objList,string Flag,int CurrentUserId,string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                foreach (var obj in objList)
                {
                    string[] Entity = {"PlanDate","DietPlanDetId","DietMealTypeId"
                };

                    var entity = obj.ToDictionary();

                    foreach (var rProperty in entity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            entity.Remove(rProperty);
                    }

                    entity["Flag"] = Flag;
                    entity["UserId"] = CurrentUserId;
                    entity["ActionDateTime"] = AppTime.Now;

                    odal.ExecuteNonQueryNew("ps_Update_Canteen_DietPlanScheduleDetailStatus",CommandType.StoredProcedure,"",entity);

                    await _context.LogProcedureExecution(entity,nameof(TDietPlanScheduleDetail),(int)obj.DietPlanDetId,Core.Domain.Logging.LogAction.Edit,CurrentUserId,CurrentUserName);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task UpdateDietPlanDateStatus(DateTime PlanDate, string Flag, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                var entity = new Dictionary<string, object>
                {
                    { "PlanDate", PlanDate },
                    { "Flag", Flag },
                    { "UserId", CurrentUserId },
                    { "ActionDateTime", AppTime.Now }
                };

                odal.ExecuteNonQueryNew("ps_Update_Canteen_DietPlanDateStatus",CommandType.StoredProcedure,"",entity);

                await _context.LogProcedureExecution(entity,nameof(TDietPlanScheduleDetail),0,Core.Domain.Logging.LogAction.Edit,CurrentUserId,CurrentUserName);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
