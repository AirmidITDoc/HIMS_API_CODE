using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;

namespace HIMS.Services.Inventory
{
    public partial interface IStoreMasterService
    {
        Task<IPagedList<StoreMasterListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
