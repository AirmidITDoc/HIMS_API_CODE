using HIMS.Core.Domain.Grid;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface IDeathCertificateService
    {
        Task<IPagedList<CertificateListDto>> CertificateListAsync(GridRequestModel objGrid);
    }
}
