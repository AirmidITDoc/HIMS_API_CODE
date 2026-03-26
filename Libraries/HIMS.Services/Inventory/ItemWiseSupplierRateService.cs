using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public  class ItemWiseSupplierRateService : IItemWiseSupplierRateService

    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemWiseSupplierRateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemWiseSupplierRateDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemWiseSupplierRateDto>(model, "PS_ItemWiseSupplierRate");
        }


    }
}
