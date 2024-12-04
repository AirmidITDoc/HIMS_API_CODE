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
    public class ItemMovementService : IItemMovementService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemMovementService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemMovementListDto>> ItemMovementList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemMovementListDto>(model, "m_rptItemMovementReport");
        }
    }
}
