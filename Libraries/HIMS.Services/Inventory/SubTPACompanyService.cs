using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public  class SubTPACompanyService :ISubTPACompanyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SubTPACompanyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SubTpaCompanyListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SubTpaCompanyListDto>(model, "ps_rtrv_SubTPACompanyMasterList");
        }

    }
}
