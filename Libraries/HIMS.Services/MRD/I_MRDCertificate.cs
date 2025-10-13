using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface I_MRDCertificate
    {
        Task<IPagedList<MRDCertificateListToDo>> MRDList(GridRequestModel objGrid);
    }
}
