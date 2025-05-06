using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class WorkOrderService : IWorkOrderService
    {
      
        private readonly Data.Models.HIMSDbContext _context;
        public WorkOrderService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WorkOrderListDto>(model, "Rtrv_WorkOrderList_by_Name");
        }

    }
}

