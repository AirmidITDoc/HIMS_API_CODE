using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.Inventory
{
    public class ApprovalService : IApprovalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ApprovalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ApprovalListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ApprovalListDto>(model, "ps_ApprovalHeaderList");
        }
        public virtual async Task<IPagedList<UserApprovalNamelistDto>> NewGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UserApprovalNamelistDto>(model, "ps_Rtrv_UserApprovalNamelist");
        }
        public virtual async Task InsertAsync(TApprovalHeader ObjTApprovalHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TApprovalHeaders
                    .OrderByDescending(x => x.ApprovalNo)
                    .Select(x => x.ApprovalNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTApprovalHeader.ApprovalNo = newSeqNo.ToString();


                ObjTApprovalHeader.CreatedBy = UserId;
                ObjTApprovalHeader.CreatedDate = AppTime.Now;

                _context.TApprovalHeaders.Add(ObjTApprovalHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
