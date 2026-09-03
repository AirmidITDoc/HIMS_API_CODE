using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.MRD
{
    public class DeathCertificateService : IDeathCertificateService
    {
        private readonly HIMSDbContext _context;
        public DeathCertificateService(HIMSDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IPagedList<CertifiCateListDto>> CertificateListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CertifiCateListDto>(model, "ps_Rtrv_MedicoDeathCertificateList");
        }
        public virtual async Task InsertAsync(TDeathCertificate objDeathCertificate, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                var certificateNos = await _context.TDeathCertificates.Where(x => x.CertificateNo != null && x.CertificateNo != "").Select(x => x.CertificateNo).ToListAsync();

                int lastSeqNo = certificateNos.Select(x => int.TryParse(x, out var n) ? n : 0).DefaultIfEmpty(0).Max();

                objDeathCertificate.CertificateNo = (lastSeqNo + 1).ToString();
                objDeathCertificate.AddedBy = UserId;
                objDeathCertificate.CertificateDate = AppTime.Now.Date;
                objDeathCertificate.CertificateTime = AppTime.Now;

                _context.TDeathCertificates.Add(objDeathCertificate);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
