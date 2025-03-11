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
            return await DatabaseHelper.GetGridDataBySp<PrescriptionReturnDto>(model, "Rtrv_IPPrescReturnItemDet");
        }
        public virtual async Task<IPagedList<PrescriptionListDto>> GetPrescriptionListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionListDto>(model, "Retrieve_PrescriptionListFromWard");
        }
        public virtual async Task<IPagedList<PrescriptionReturnListDto>> GetListAsyncReturn(GridRequestModel model)
        {
            //var qry = from t in _context.TIpprescriptionReturnHs
            //          join a in _context.Admissions on t.OpIpId equals a.AdmissionId
            //          join r in _context.Registrations on a.RegId equals r.RegId
            //          join p in _context.DbPrefixMasters on r.PrefixId equals p.PrefixId
            //          join s in _context.MStoreMasters on t.ToStoreId equals s.StoreId
            //          select new PrescriptionReturnListDto()
            //          {
            //              PatientName = p.PrefixName + " " + r.FirstName + " " + r.MiddleName + " " + r.LastName,
            //              RegNo = r.RegNo,
            //              PresReId = t.PresReId,
            //              Date = (t.PresDate.Value.ToString("dd/MM/yyyy") ?? ""),
            //              PresTime = t.PresTime,
            //              OP_IP_Id = t.OpIpId,
            //              Vst_Adm_Date = (a.AdmissionDate.Value.ToString("dd/MM/yyyy") ?? ""),
            //              StoreName = s.StoreName,
            //              OP_IP_Type = t.OpIpType
            //          };
            //return await qry.BuildPredicate(model);
            return await DatabaseHelper.GetGridDataBySp<PrescriptionReturnListDto>(model, "Rtrv_IPPrescriptionReturnListFromWard");
        }
        public virtual async Task<IPagedList<PrescriptionDetailListDto>> GetListAsyncDetail(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionDetailListDto>(model, "m_Rtrv_IP_Prescriptio_Det");
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
        public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
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


