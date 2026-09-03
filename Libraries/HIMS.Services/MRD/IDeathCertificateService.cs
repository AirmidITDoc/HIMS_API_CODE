using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
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
        Task InsertAsync(TDeathCertificate objDeathCertificate, int UserId, string Username);

    }
}
