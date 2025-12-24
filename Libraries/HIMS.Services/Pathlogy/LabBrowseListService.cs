using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Services.OTManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class LabBrowseListService : ILabBrowseListService
    {
        public async Task<IPagedList<BrowseLABBillListDto>> GetLabListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper
                .GetGridDataBySp<BrowseLABBillListDto>(model, "ps_Rtrv_BrowseLABBill_Pagi");
        }
    }

}
