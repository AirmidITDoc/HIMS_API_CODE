using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.MRD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface IDeathCertificateService
    {
        Task<IPagedList<CertifiCateListDto>> CertificateListAsync(GridRequestModel objGrid);
    }
}
