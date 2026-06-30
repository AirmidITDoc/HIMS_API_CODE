using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public  class ExternalDoctorService : IExternalDoctorService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ExternalDoctorService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ExternalDoctorMasterDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ExternalDoctorMasterDto>(model, "ps_rtrv_ExternalDoctorMaster");
        }
    }
}
