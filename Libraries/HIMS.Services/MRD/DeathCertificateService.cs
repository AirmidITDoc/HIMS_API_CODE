using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public class DeathCertificateService : IDeathCertificateService
    {
        private readonly HIMSDbContext _context;
        public DeathCertificateService(HIMSDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IPagedList<CertificateListDto>> CertificateListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CertificateListDto>(model, "ps_rtrv_Medico_Death_Certificate_List");
        }
    }
}
