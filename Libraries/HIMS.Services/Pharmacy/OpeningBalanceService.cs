using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class OpeningBalanceService :IOpeningBalanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OpeningBalanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OpeningBalListDto>> GetOpeningBalanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpeningBalListDto>(model, "m_Rtrv_OpeningItemList");
        }
        public virtual async Task<IPagedList<OpeningBalanaceItemDetailListDto>> GetOPningBalItemDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpeningBalanaceItemDetailListDto>(model, "m_Rtrv_OpeningItemDet");
        }
    }
}
