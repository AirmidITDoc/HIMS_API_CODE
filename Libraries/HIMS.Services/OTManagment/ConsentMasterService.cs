using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class ConsentMasterService : IConsentMasterService
    {
        private readonly HIMSDbContext _context;
        public ConsentMasterService(HIMSDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IPagedList<ConsentMasterDto>> ConsentMasterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ConsentMasterDto>(model, "ps_ConsentMasterList");
        }
    }
}
