using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PharmacyReorderService:IPharmacyReorderService
    {
        private readonly HIMSDbContext _context;
        public PharmacyReorderService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
     
        public virtual async Task<IPagedList<ItemReorderListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemReorderListDto>(model, "ps_rtrvItemReorderList");
        }
    }
}
