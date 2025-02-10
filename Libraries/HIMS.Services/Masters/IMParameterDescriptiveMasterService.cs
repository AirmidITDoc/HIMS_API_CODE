using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Masters
{
    public partial interface IMParameterDescriptiveMasterService
    {
        Task<IPagedList<MParameterDescriptiveMasterListDto>> GetListAsync(GridRequestModel objGrid);
    }
}
