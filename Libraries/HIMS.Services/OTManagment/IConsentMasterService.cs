using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OTManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IConsentMasterService
    {
        Task<IPagedList<ConsentMasterDto>> ConsentMasterListAsync(GridRequestModel objGrid);
    }
}
