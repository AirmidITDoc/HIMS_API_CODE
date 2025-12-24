using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Master;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public partial interface IBedMasterService
    {
        Task UpdateAsync(Bedmaster ObjBedmaster,  int CurrentUserId, string CurrentUserName);
        Task<IPagedList<BedmasterListDto>> GetBedListListAsync(GridRequestModel objGrid);

    }
}
