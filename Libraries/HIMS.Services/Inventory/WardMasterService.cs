using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public  class WardMasterService : IWardMasterService
    {

        private readonly Data.Models.HIMSDbContext _context;
        public WardMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<WardMasterListDto>> GetListAsyncH(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WardMasterListDto>(model, "Rtrv_WardDetailList");
        }
    }
}
