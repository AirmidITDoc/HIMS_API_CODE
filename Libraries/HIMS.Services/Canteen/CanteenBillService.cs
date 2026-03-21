using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace HIMS.Services.Canteen
{
    public class CanteenBillService : ICanteenBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CanteenBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<CanteenBillDetailsLisDto>> CanteenBilldetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenBillDetailsLisDto>(model, "Rtrv_CanteenBillListDetail");
        }

        public virtual async Task<IPagedList<CanteenBillListDo>> CanteenBillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenBillListDo>(model, "rtrv_CanteenBillList");
        }

        public virtual async Task InsertAsync(TCanteenBillHeader ObjTCanteenBillHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TCanteenBillHeaders
                    .OrderByDescending(x => x.PbillNo)
                    .Select(x => x.PbillNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTCanteenBillHeader.PbillNo = newSeqNo.ToString();

                ObjTCanteenBillHeader.CreatedBy = UserId;
                ObjTCanteenBillHeader.CreatedDate = AppTime.Now;

                _context.TCanteenBillHeaders.Add(ObjTCanteenBillHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
       
        public virtual async Task UpdateAsync(TCanteenBillHeader ObjTCanteenBillHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                long vBillNo = ObjTCanteenBillHeader.BillNo;

                var lstAttend = await _context.TCanteenBillDetails
                    .Where(x => x.BillNo == vBillNo)
                    .ToListAsync();

                if (lstAttend.Any()) _context.TCanteenBillDetails.RemoveRange(lstAttend);

                await _context.SaveChangesAsync();

                //  attach and update header
                _context.Attach(ObjTCanteenBillHeader);
                _context.Entry(ObjTCanteenBillHeader).State = EntityState.Modified;

                _context.Entry(ObjTCanteenBillHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTCanteenBillHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTCanteenBillHeader).Property(x => x.PbillNo).IsModified = false;

                ObjTCanteenBillHeader.ModifiedBy = UserId;
                ObjTCanteenBillHeader.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTCanteenBillHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
