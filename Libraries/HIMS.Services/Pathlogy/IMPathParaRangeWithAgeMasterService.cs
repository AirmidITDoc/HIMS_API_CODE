using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IMPathParaRangeWithAgeMasterService
    {
        Task<IPagedList<MPathParaRangeWithAgeMasterListDto>> MPathParaRangeWithAgeMasterList(GridRequestModel objGrid);

       

    }
}
