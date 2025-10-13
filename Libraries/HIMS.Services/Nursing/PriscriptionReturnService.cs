using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class PriscriptionReturnService : IPriscriptionReturnService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PriscriptionReturnService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PrescriptionReturnDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionReturnDto>(model, "ps_Rtrv_IPPrescReturnItemDet");
        }
        public virtual async Task<IPagedList<PrescriptionListDto>> GetPrescriptionListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionListDto>(model, "ps_Rtrv_IPPrescriptionListFromWard");
        }
        public virtual async Task<IPagedList<PrescriptionReturnListDto>> GetListAsyncReturn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionReturnListDto>(model, "ps_Rtrv_IPPrescriptionReturnListFromWard");
        }
        public virtual async Task<IPagedList<PrescriptionDetailListDto>> GetListAsyncDetail(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionDetailListDto>(model, "ps_Rtrv_IP_Prescriptio_Det");
        }



        public virtual async Task InsertAsync(TIpPrescription ObjTIpPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpPrescriptions.Add(ObjTIpPrescription);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task UpdateAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TIpprescriptionReturnHs.Update(objIpprescriptionReturnH);
                _context.Entry(objIpprescriptionReturnH).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}


