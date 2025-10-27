using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public class ItemWiseService : IItemWiseService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemWiseService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemWiseListDto>> ItemWiseList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemWiseListDto>(model, "m_rpt_ItemWiseSalesReport");
        }
    }
}
