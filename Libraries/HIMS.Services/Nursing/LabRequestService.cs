using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await DatabaseHelper.GetGridDataBySp<LabRequestListDto>(model, "m_Rtrv_LabRequest_Nursing");

        }
        public virtual async Task<IPagedList<LabRequestDetailsListDto>> SPGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabRequestDetailsListDto>(model, "m_Rtrv_NursingLabRequestDetails");

        }
       


        public virtual async Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.THlabRequests.Add(objTHlabRequest);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
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
                ObjLab.IsCancelledDate = DateTime.Now.Date;
                ObjLab.IsCancelledTime = DateTime.Now;

                _context.THlabRequests.Update(ObjLab);
                _context.Entry(ObjLab).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        
    }

}
