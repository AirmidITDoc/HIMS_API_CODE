using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial  interface IWorkOrderService
    {
        Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel objGrid);
    }
}
