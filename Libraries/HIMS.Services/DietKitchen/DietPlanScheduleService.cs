using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
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
            return await DatabaseHelper.GetGridDataBySp<DietPlanDetailListDto>(model, "ps_Rtrv_DietplanDetails");
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

            var lstDetails = await _context.TDietPlanScheduleDetails.Where(x => x.DietPlanId == dietPlanId).ToListAsync();
            if (lstDetails.Any())
                _context.TDietPlanScheduleDetails.RemoveRange(lstDetails);

            await _context.SaveChangesAsync();

            _context.Attach(objDietPlanSchedule);
            _context.Entry(objDietPlanSchedule).State = EntityState.Modified;

            objDietPlanSchedule.ModifiedBy = UserId;
            objDietPlanSchedule.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(objDietPlanSchedule).Property(column).IsModified = false;
            }

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

        public virtual async Task Cancel(TDietPlanSchedule objDietPlanSchedule, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                string[] Entity = { "DietPlanId", "IsCancelledBy", "IsCancelledDate" };
                var entity = objDietPlanSchedule.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                entity["IsCancelledDate"] = DateTime.Now;

                odal.ExecuteNonQueryNew("ps_DietPlanscheduleCancel", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(TDietPlanSchedule), (int)objDietPlanSchedule.DietPlanId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task CancelD(TDietPlanScheduleDetail objDietPlanScheduleDetail, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

               
                string[] Entity = { "DietPlanDetId", "IsCancelledBy", "IsCancelledDate", "CancelledReason" };
                var entity = objDietPlanScheduleDetail.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                entity["IsCancelledDate"] = DateTime.Now;

                odal.ExecuteNonQueryNew("ps_DietPlanscheduledetailCancel", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(TDietPlanScheduleDetail), (int)objDietPlanScheduleDetail.DietPlanDetId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task<IPagedList<DietMainlListDto>> GetMainListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietMainlListDto>(model, "ps_Rtrv_Dietschdule_mainlist");
        }
        public virtual async Task<IPagedList<DietMainSublListDto>> GetMainSubListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietMainSublListDto>(model, "ps_Rtrv_Dietschdule_mainsublist");
        }
        public virtual async Task<IPagedList<DietPlanDetailListDto>> DietPlanScheduleDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPlanDetailListDto>(model, "ps_Rtrv_Schedule_DietplanDetails");
        }
        public virtual async Task<IPagedList<DietSchedulePatientListDto>> DietPlanSchedulePatientListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietSchedulePatientListDto>(model, "ps_Rtrv_Dietschdule_patientlist");
        }
        public virtual async Task UpdateDietPlanScheduleStatus(List<DietPlanScheduleStatusUpdateModel> objList, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                foreach (var obj in objList)
                {
                    string[] Entity = { "DietPlanIds", "Flag" };

                    var entity = obj.ToDictionary();

                    foreach (var rProperty in entity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            entity.Remove(rProperty);
                    }

                    entity["DietPlanId"] = entity["DietPlanIds"];
                    entity.Remove("DietPlanIds");

                    entity["UserId"] = CurrentUserId;
                    entity["ActionDateTime"] = AppTime.Now;

                    odal.ExecuteNonQueryNew("ps_Update_DietPlanScheduleDetailStatus", CommandType.StoredProcedure, "", entity);

                    await _context.LogProcedureExecution(entity, nameof(TDietPlanScheduleDetail), Convert.ToInt32(obj.DietPlanIds), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
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
    }
}