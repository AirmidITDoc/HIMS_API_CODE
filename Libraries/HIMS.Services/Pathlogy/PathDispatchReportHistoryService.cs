using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pathlogy
{
    public partial class PathDispatchReportHistoryService: IPathDispatchReportHistoryService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PathDispatchReportHistoryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PathDispatchReportHistoryListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathDispatchReportHistoryListDto>(model, "ps_PathDispatchReportHistory");
        }
        public virtual async Task<IPagedList<TestDispatchModelDto>> TestGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TestDispatchModelDto>(model, "ps_Rtrv_dispatchTestList");
        }
        public virtual async Task InsertAsync(TPathDispatchReportHistory ObjTPathDispatchReportHistory, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            _context.TPathDispatchReportHistories.Add(ObjTPathDispatchReportHistory);
            await _context.SaveChangesAsync();

            scope.Complete();

        }
        public virtual async Task UpdateAsync(TPathDispatchReportHistory ObjTPathDispatchReportHistory, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                long reservationId = ObjTPathDispatchReportHistory.DispatchId;

                // Delete related details first
                var lstAttend = await _context.TOtReservationAttendingDetails
                    .Where(x => x.OtreservationId == reservationId).ToListAsync();
                if (lstAttend.Any())
                    _context.TOtReservationAttendingDetails.RemoveRange(lstAttend);

                
                // Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTPathDispatchReportHistory);
                _context.Entry(ObjTPathDispatchReportHistory).State = EntityState.Modified;

                _context.Entry(ObjTPathDispatchReportHistory).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTPathDispatchReportHistory).Property(x => x.CreatedDate).IsModified = false;

                ObjTPathDispatchReportHistory.ModifiedBy = UserId;
                ObjTPathDispatchReportHistory.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTPathDispatchReportHistory).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }



    }
}
