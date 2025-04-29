using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;

namespace HIMS.Services.Inventory
{
    public  class StoreMasterService : IStoreMasterService
    {

        public virtual async Task<IPagedList<StoreMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<StoreMasterListDto>(model, "Rtrv_StoreMaster_by_Name");
        }
    }
}
