using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class LabRequestService : ILabRequestService
    {

        private readonly Data.Models.HIMSDbContext _context;
        public LabRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LabRequestListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabRequestListDto>(model, "ps_Rtrv_LabRequest_Nursing");

        }
        public virtual async Task<IPagedList<LabRequestDetailsListDto>> SPGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabRequestDetailsListDto>(model, "ps_Rtrv_NursingLabRequestDetails");

        }



        //public virtual async Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        var lastSeqNoStr = await _context.THlabRequests
        //           .OrderByDescending(x => x.ReqNo)
        //           .Select(x => x.ReqNo)
        //           .FirstOrDefaultAsync();

        //        int lastSeqNo = 0;
        //        if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
        //            lastSeqNo = parsed;

        //        // Increment the sequence number
        //        int newSeqNo = lastSeqNo + 1;
        //        objTHlabRequest.ReqNo = newSeqNo.ToString();
        //        _context.THlabRequests.Add(objTHlabRequest);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled);

            var presNoList = await _context.THlabRequests
                .Where(x => x.ReqNo != null && x.ReqNo != "")
                .Select(x => x.ReqNo)
                .ToListAsync();

            int lastPresNo = presNoList
                .Select(p => int.TryParse(p, out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            //  Increment & assign
            objTHlabRequest.ReqNo = (lastPresNo + 1).ToString();
            _context.THlabRequests.Add(objTHlabRequest);
            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task CancelAsync(THlabRequest objTHlabRequest, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                THlabRequest ObjLab = await _context.THlabRequests.FindAsync(objTHlabRequest.RequestId);
                if (ObjLab == null)
                    throw new Exception("Lab request not found.");
                // Cancel fields
                ObjLab.IsCancelled = true;
                ObjLab.IsCancelledBy = CurrentUserId;
                ObjLab.IsCancelledDate = AppTime.Now.Date;
                ObjLab.IsCancelledTime = AppTime.Now;

                _context.THlabRequests.Update(ObjLab);
                _context.Entry(ObjLab).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }

}
